using AutoMapper;
using LearnPlatform.API.DTOs;
using LearnPlatform.API.Models;

namespace LearnPlatform.API.Mappings;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        // Course mappings
        CreateMap<Course, CourseDto>()
            .ForMember(dest => dest.InstructorName,
                opt => opt.MapFrom(src => src.Instructor.Username))
            .ForMember(dest => dest.LessonCount,
                opt => opt.MapFrom(src => src.Lessons.Count))
            .ForMember(dest => dest.EnrollmentCount,
                opt => opt.MapFrom(src => src.Enrollments.Count));

        CreateMap<CreateCourseDto, Course>();

        // Lesson mappings
        CreateMap<Lesson, LessonDto>();
        CreateMap<CreateLessonDto, Lesson>();

        // Enrollment mappings
        CreateMap<Enrollment, EnrollmentDto>()
            .ForMember(dest => dest.CourseTitle,
                opt => opt.MapFrom(src => src.Course.Title));

        // User mappings
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Bio,
                opt => opt.MapFrom(src => src.Profile != null ? src.Profile.Bio : null));
    }
}