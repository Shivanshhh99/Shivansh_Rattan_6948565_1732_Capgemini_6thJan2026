let tasks = [];

function handleKeyDown(event) {
  if (event.key === 'Enter') addTask();
}

function addTask() {
  const input = document.getElementById('taskInput');
  const text = input.value.trim();
  if (!text) return;

  const task = { id: Date.now(), text, done: false };
  tasks.push(task);
  input.value = '';
  renderTasks();
}

function toggleTask(id) {
  const task = tasks.find(t => t.id === id);
  if (task) task.done = !task.done;
  renderTasks();
}

function deleteTask(id) {
  tasks = tasks.filter(t => t.id !== id);
  renderTasks();
}

function clearAll() {
  if (tasks.length === 0) return;
  if (confirm('Clear all tasks?')) {
    tasks = [];
    renderTasks();
  }
}

function clearDone() {
  tasks = tasks.filter(t => !t.done);
  renderTasks();
}

function renderTasks() {
  const list = document.getElementById('taskList');
  const emptyState = document.getElementById('emptyState');
  const totalCount = document.getElementById('totalCount');
  const doneCount = document.getElementById('doneCount');

  list.innerHTML = '';

  const done = tasks.filter(t => t.done).length;
  totalCount.textContent = `${tasks.length} task${tasks.length !== 1 ? 's' : ''}`;
  doneCount.textContent = `${done} done`;

  if (tasks.length === 0) {
    emptyState.classList.add('visible');
    return;
  }

  emptyState.classList.remove('visible');

  tasks.forEach(task => {
    const li = document.createElement('li');
    li.className = 'task-item' + (task.done ? ' done' : '');
    li.innerHTML = `
      <div class="task-checkbox"></div>
      <span class="task-text">${escapeHTML(task.text)}</span>
      <button class="delete-btn" title="Delete task">✕</button>
    `;
    li.querySelector('.task-checkbox').addEventListener('click', () => toggleTask(task.id));
    li.querySelector('.task-text').addEventListener('click', () => toggleTask(task.id));
    li.querySelector('.delete-btn').addEventListener('click', (e) => {
      e.stopPropagation();
      deleteTask(task.id);
    });
    list.appendChild(li);
  });
}

function escapeHTML(str) {
  return str.replace(/&/g,'&amp;').replace(/</g,'&lt;').replace(/>/g,'&gt;');
}

// Initial render
renderTasks();