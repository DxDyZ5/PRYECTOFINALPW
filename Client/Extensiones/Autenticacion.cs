﻿using PROYECTOFINALPW.Shared;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace PROYECTOFINALPW.Client.Extensiones
{
    public class Autenticacion : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorage;
        private ClaimsPrincipal _sinInformacion = new ClaimsPrincipal(new ClaimsIdentity());

        public Autenticacion(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }



        public async Task ActualizarEstadoAutenticacion(Sesion? sesionUsuario)
        {
            ClaimsPrincipal claimsPrincipal;

            if (sesionUsuario != null)
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name,sesionUsuario.Nombre),
                    new Claim(ClaimTypes.UserData,sesionUsuario.Usuario), // CHANGED HERE LOOK AFTER
                    new Claim(ClaimTypes.Role,sesionUsuario.Rol)    
                }, "JwtAuth"));

                await _sessionStorage.GuardarStorage("sesionUsuario", sesionUsuario);
            }
            else
            {
                claimsPrincipal = _sinInformacion;
                await _sessionStorage.RemoveItemAsync("sesionUsuario");
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));

        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var sesionUsuario = await _sessionStorage.ObtenerStorage<Sesion>("sesionUsuario");

            if (sesionUsuario == null)
                return await Task.FromResult(new AuthenticationState(_sinInformacion));

            var claimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name,sesionUsuario.Nombre),
                    new Claim(ClaimTypes.UserData,sesionUsuario.Usuario), //changeHERE TOO LOOK AFTER
                    new Claim(ClaimTypes.Role,sesionUsuario.Rol)
                }, "JwtAuth"));


            return await Task.FromResult(new AuthenticationState(claimPrincipal));
        }
    }
}
