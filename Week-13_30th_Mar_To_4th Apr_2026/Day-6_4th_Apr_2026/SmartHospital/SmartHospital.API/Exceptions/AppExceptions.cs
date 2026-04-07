namespace SmartHospital.API.Exceptions;

public class NotFoundException : KeyNotFoundException
{
    public NotFoundException(string resource, int id)
        : base($"{resource} with ID {id} was not found.") { }
}

public class DuplicateEmailException : InvalidOperationException
{
    public DuplicateEmailException(string email)
        : base($"A user with email '{email}' already exists.") { }
}

public class AppointmentConflictException : InvalidOperationException
{
    public AppointmentConflictException()
        : base("Doctor already has an appointment at this time.") { }
}