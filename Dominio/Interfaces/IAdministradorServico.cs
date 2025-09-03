using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MinimalAPI.Dominio.DTO;
using MinimalAPI.Dominio.Entidades;

namespace MinimalAPI.Dominio.Interfaces
{
    public interface IAdministradorServico
    {
        Administrador? Login(LoginDTO loginDTO);
    }
}