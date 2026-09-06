using AutoMapper;
using GimnasioAPI.DTOs.Clase;
using GimnasioAPI.DTOs.ClaseInstructor;
using GimnasioAPI.DTOs.Disciplina;
using GimnasioAPI.DTOs.Instructor;
using GimnasioAPI.DTOs.Resena;
using GimnasioAPI.Entidades;

namespace GimnasioAPI.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Disciplina, DisciplinaDTO>();
            CreateMap<Disciplina, DisciplinaConClasesDTO>();
            CreateMap<DisciplinaCreacionDTO, Disciplina>();
            CreateMap<Disciplina, DisciplinaPatchDTO>().ReverseMap();

            CreateMap<Instructor, InstructorDTO>().ForMember(dto => dto.NombreCompleto, config => config.MapFrom(ent => $"{ent.Nombres} {ent.Apellidos}"));
            CreateMap<Instructor, InstructorConClasesDTO>().ForMember(dto => dto.NombreCompleto, config => config.MapFrom(ent => $"{ent.Nombres} {ent.Apellidos}"));
            CreateMap<InstructorCreacionDTO, Instructor>();
            CreateMap<InstructorPatchDTO, Instructor>().ReverseMap();

            CreateMap<Clase, ClaseDTO>();
            CreateMap<Clase, ClaseConHijosDTO>();
            CreateMap<ClaseCreacionDTO, Clase>();
            CreateMap<Clase, ClasePatchDTO>().ReverseMap();

            CreateMap<ClaseInstructorCreacionDTO, ClaseInstructor>();
            CreateMap<ClaseInstructor, ClaseInstructorDTO>().ForMember(dto => dto.NombreCompleto, config => config.MapFrom(ent => $"{ent.Instructor!.Nombres} {ent.Instructor!.Apellidos}"));
            CreateMap<ClaseInstructor, InstructorClaseDTO>().ForMember(dto => dto.NombreClase, config => config.MapFrom(ent => $"{ent.Clase!.Nombre}"));

            CreateMap<Resena, ResenaDTO>();
        }
    }
}
