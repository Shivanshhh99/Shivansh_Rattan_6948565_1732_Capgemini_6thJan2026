using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public string Name { get; set; } = null!;

    public string Specialization { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
