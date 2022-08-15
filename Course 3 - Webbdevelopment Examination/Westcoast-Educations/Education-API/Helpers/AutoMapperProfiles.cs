using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Education_API.Models;
using Education_API.ViewModels;
using Education_API.ViewModels.Authorization;
using Education_API.ViewModels.Categories;
using Education_API.ViewModels.Courses;
using Education_API.ViewModels.Skills;
using Education_API.ViewModels.Students;
using Education_API.ViewModels.Teachers;

namespace Education_API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
    {
        // Course
        CreateMap<Course, string>()
        .ConvertUsing(src => src.Title ?? String.Empty);     
        CreateMap<PostCourseViewModel, Course>();
        CreateMap<Course, CourseViewModel>()
        .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
        .ForMember(dest => dest.Category, options => options.MapFrom(src => src.Category.Name));

        // Category
        CreateMap<Category, string>()
        .ConvertUsing(src => src.Name ?? String.Empty); 
        CreateMap<PostCategoryViewModel, Category>();
        CreateMap<Category, CategoryViewModel>()
        .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id));

        // Teacher
        CreateMap<PostTeacherViewModel, RegisterUserViewModel>();
        CreateMap<PatchTeacherViewModel, PatchUserViewModel>();
        CreateMap<PatchTeacherViewModel, Teacher>();
        CreateMap<PostTeacherViewModel, Teacher>();
        CreateMap<Teacher, TeacherViewModel>()
        .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
        .ForMember(dest => dest.Email, options => options.MapFrom(src=> src.IdentityUser.Email))
        .ForMember(dest => dest.PhoneNumber, options => options.MapFrom(src=> src.IdentityUser.PhoneNumber));
        
        // Student
        CreateMap<PostStudentViewModel, RegisterUserViewModel>(); 
        CreateMap<PatchStudentViewModel, PatchUserViewModel>();
        CreateMap<PatchStudentViewModel, Student>();
        CreateMap<PostStudentViewModel, Student>();
        CreateMap<Student, StudentViewModel>()
        .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
        .ForMember(dest => dest.Email, options => options.MapFrom(src=> src.IdentityUser!.Email))
        .ForMember(dest => dest.PhoneNumber, options => options.MapFrom(src=> src.IdentityUser!.PhoneNumber));

        // Skill
        CreateMap<PostSkillViewModel, Skill>();
        CreateMap<Skill, SkillViewModel>();   
        CreateMap<Skill, string>()
        .ConvertUsing(src => src.Name ?? String.Empty);     



    }

    }
}