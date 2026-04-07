using AutoMapper;
using SmartHospital.API.DTOs.Appointment;
using SmartHospital.API.DTOs.Billing;
using SmartHospital.API.DTOs.Doctor;
using SmartHospital.API.DTOs.Prescription;
using SmartHospital.API.Models;

namespace SmartHospital.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Doctor mappings
        CreateMap<Doctor, DoctorDto>()
            .ForMember(d => d.FullName, o => o.MapFrom(src => src.User.FullName))
            .ForMember(d => d.Email, o => o.MapFrom(src => src.User.Email))
            .ForMember(d => d.DepartmentName, o => o.MapFrom(src => src.Department.DepartmentName));

        // Appointment mappings
        CreateMap<Appointment, AppointmentDto>()
            .ForMember(d => d.PatientName, o => o.MapFrom(src => src.Patient.FullName))
            .ForMember(d => d.DoctorName, o => o.MapFrom(src => src.Doctor.User.FullName))
            .ForMember(d => d.DepartmentName, o => o.MapFrom(src => src.Doctor.Department.DepartmentName))
            .ForMember(d => d.HasPrescription, o => o.MapFrom(src => src.Prescription != null))
            .ForMember(d => d.HasBill, o => o.MapFrom(src => src.Bill != null));

        CreateMap<CreateAppointmentDto, Appointment>();

        // Prescription mappings
        CreateMap<Prescription, PrescriptionDto>()
            .ForMember(d => d.PatientName, o => o.MapFrom(src => src.Appointment.Patient.FullName))
            .ForMember(d => d.DoctorName, o => o.MapFrom(src => src.Appointment.Doctor.User.FullName))
            .ForMember(d => d.AppointmentDate, o => o.MapFrom(src => src.Appointment.AppointmentDate));

        CreateMap<CreatePrescriptionDto, Prescription>();

        // Bill mappings
        CreateMap<Bill, BillDto>()
            .ForMember(d => d.PatientName, o => o.MapFrom(src => src.Appointment.Patient.FullName))
            .ForMember(d => d.DoctorName, o => o.MapFrom(src => src.Appointment.Doctor.User.FullName))
            .ForMember(d => d.AppointmentDate, o => o.MapFrom(src => src.Appointment.AppointmentDate))
            .ForMember(d => d.TotalAmount, o => o.MapFrom(src => src.ConsultationFee + src.MedicineCharges));

        CreateMap<CreateBillDto, Bill>();
    }
}