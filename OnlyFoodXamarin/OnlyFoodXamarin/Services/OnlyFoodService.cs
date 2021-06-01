using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlyFoodXamarin.Helpers;
using OnlyFoodXamarin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OnlyFoodXamarin.Services
{
    public class OnlyFoodService
    {
        private String url;
        MediaTypeWithQualityHeaderValue header;
        UploadService uploadService;
        Uri UriApi;
        public OnlyFoodService(UploadService uploadService)
        {
            //this.url = "http://ec2-44-192-39-168.compute-1.amazonaws.com/";
            this.url = "https://apionlyfood.azurewebsites.net/";
            //this.url = "https://0yya80zw7i.execute-api.us-east-1.amazonaws.com/Prod/";
            //this.url = "https://localhost:44399/";
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
            this.uploadService = uploadService;
            this.UriApi = new Uri(this.url);
        }
        #region LLAMADAS API
        private async Task<T> CallApi<T>(String request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    //T data = await response.Content.ReadAsAsync<T>();
                    //return data;
                    String data =
                        await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(data);
                }
                else
                {
                    return default(T);
                }
            }
        }
        private async Task<T> CallApi<T>(String request, String token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    //T data = await response.Content.ReadAsAsync<T>();
                    //return data;
                    String data =
                        await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(data);
                }
                else
                {
                    return default(T);
                }
            }
        }
        private async Task<T> PostApi<T>(String request, T objeto)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                String json = JsonConvert.SerializeObject(objeto);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    //T data = await response.Content.ReadAsAsync<T>();
                    //return data;
                    String data =
                        await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(data);
                }
                else
                {
                    return default(T);
                }
            }
        }
        private async Task<T> PostApi<T>(String request, T objeto, String token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                String json = JsonConvert.SerializeObject(objeto);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    //T data = await response.Content.ReadAsAsync<T>();
                    //return data;
                    String data =
                        await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(data);
                }
                else
                {
                    return default(T);
                }
            }
        }
        private async Task<T> PutApi<T>(String request, T objeto)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                //client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                String json = JsonConvert.SerializeObject(objeto);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PutAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    //T data = await response.Content.ReadAsAsync<T>();
                    //return data;
                    String data =
                        await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(data);
                }
                else
                {
                    return default(T);
                }
            }
        }
        private async Task<T> PutApi<T>(String request, T objeto, String token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                String json = JsonConvert.SerializeObject(objeto);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PutAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    //T data = await response.Content.ReadAsAsync<T>();
                    //return data;
                    String data =
                        await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(data);
                }
                else
                {
                    return default(T);
                }
            }
        }
        private async Task DeleteApi(String request, String token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                await client.DeleteAsync(request);
            }
        }
        #endregion
        #region CADENAS
        public async Task<List<Cadena>> GetCadenasAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                String request = "/api/Cadenas";
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    //List<Cadena> cadenas =
                    //    await response.Content.ReadAsAsync<List<Cadena>>();
                    //return cadenas;
                    String data =
                        await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Cadena>>(data);
                }
                else
                {
                    return null;
                }

            }
        }
        public async Task<Cadena> GetCadenaByIdAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                String request = "/api/Cadenas/" + id;
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    //Cadena cadena =
                    //    await response.Content.ReadAsAsync<Cadena>();
                    //return cadena;
                    String data =
                        await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Cadena>(data);
                }
                else
                {
                    return null;
                }

            }
        }
        public async Task NewCadenaAsync(string nombre, string descripcion,
            IFormFile imagen, string web, String token)
        {
            Cadena c = new Cadena();
            c.Nombre = nombre;
            c.Descripcion = descripcion;
            //String name = await this.uploadService.UploadFileAsycn(imagen, Folders.Images);
            String name = await this.uploadService.UploadImageBlobAzureAsycn(imagen);
            //using (var memoryStream = new MemoryStream())
            //{
            //    await imagen.CopyToAsync(memoryStream);
            //    bool respuesta = await this.uploadService.UploadFileAWSAsync(memoryStream, imagen.FileName);
            //}
            c.Imagen = imagen.FileName;
            //c.Imagen = name;
            c.Web = web;
            using (HttpClient client = new HttpClient())
            {
                String request = "api/Cadenas/";
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                String json = JsonConvert.SerializeObject(c);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                await client.PostAsync(request, content);
            }
        }
        public async Task DeleteCadenaAsync(int id, String token)
        {
            using (HttpClient client = new HttpClient())
            {
                String request = "api/Cadenas/" + id;
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                await client.DeleteAsync(request);
            }
        }
        #endregion
        #region OFERTAS
        public async Task<List<Oferta>> GetAllOfertasAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                String request = "api/Ofertas";
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    //List<Oferta> ofertas =
                    //    await response.Content.ReadAsAsync<List<Oferta>>();
                    //return ofertas;
                    String data =
                        await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Oferta>>(data);
                }
                else
                {
                    return null;
                }

            }
        }
        public async Task<Oferta> GetOfertaByIdAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                String request = "api/Ofertas/" + id;
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    //Oferta oferta =
                    //    await response.Content.ReadAsAsync<Oferta>();
                    //return oferta;
                    String data =
                        await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Oferta>(data);
                }
                else
                {
                    return null;
                }

            }
        }
        public async Task<OfertasListApi> GetOfertasPaginadosAsync(FiltroOfertas filtro, int posicion, int elem)
        {
            using (HttpClient client = new HttpClient())
            {
                String request = "api/Ofertas/GetOfertasPaginados/" + posicion + "/" + elem;
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                String json = JsonConvert.SerializeObject(filtro);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    //OfertasListApi ofertaList =
                    //    await response.Content.ReadAsAsync<OfertasListApi>();
                    //return ofertaList;
                    String data =
                        await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<OfertasListApi>(data);
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task<OfertasListApi> GetOfertasByFiltroAsync(FiltroOfertas filtro)
        {
            using (HttpClient client = new HttpClient())
            {
                String request = "api/Ofertas/GetOfertasByFiltro/";
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                String json = JsonConvert.SerializeObject(filtro);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    //OfertasListApi ofertaList =
                    //    await response.Content.ReadAsAsync<OfertasListApi>();
                    //return ofertaList;
                    String data =
                        await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<OfertasListApi>(data);
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task<List<Oferta>> GetOfertasUserByFiltroAsync(int iduser, FiltroOfertas filtro)
        {
            using (HttpClient client = new HttpClient())
            {
                String request = "api/Ofertas/GetOfertasUserByFiltro/" + iduser;
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                String json = JsonConvert.SerializeObject(filtro);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    //List<Oferta> ofertaList =
                    //    await response.Content.ReadAsAsync<List<Oferta>>();
                    //return ofertaList;
                    String data =
                        await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Oferta>>(data);
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task<List<Oferta>> GetOfertasByCadenaAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                String request = "/api/Ofertas/GetOfertasByCadena/" + id;
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    //List<Oferta> ofertas =
                    //    await response.Content.ReadAsAsync<List<Oferta>>();
                    //return ofertas;
                    String data =
                        await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Oferta>>(data);
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task<List<Oferta>> GetOfertasByEmailAsync(String email, String token)
        {
            using (HttpClient client = new HttpClient())
            {
                String request = "/api/Ofertas/GetOfertasByEmail/" + email;
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    //List<Oferta> ofertas =
                    //    await response.Content.ReadAsAsync<List<Oferta>>();
                    //return ofertas;
                    String data =
                        await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Oferta>>(data);
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task<List<Oferta>> GetOfertasByIdUserAsync(int id, String token)
        {
            using (HttpClient client = new HttpClient())
            {
                String request = "/api/Ofertas/GetOfertasByIdUser/" + id;
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    //List<Oferta> ofertas =
                    //    await response.Content.ReadAsAsync<List<Oferta>>();
                    //return ofertas;
                    String data =
                        await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Oferta>>(data);
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task NewOfertaAsync(int cadena, string titulo, string descripcion,
            IFormFile imagen, string web, string codigo, double precio, int usuario, String token)
        {
            Oferta oferta = new Oferta();
            oferta.IdCadena = cadena;
            oferta.Titulo = titulo;
            oferta.Descripcion = descripcion;
            if (imagen != null)
            {
                //String name = await this.uploadService.UploadFileAsycn(imagen, Folders.Images);
                String name = await this.uploadService.UploadImageBlobAzureAsycn(imagen);
                //using (var memoryStream = new MemoryStream())
                //{
                //    await imagen.CopyToAsync(memoryStream);
                //    bool respuesta = await this.uploadService.UploadFileAWSAsync(memoryStream, imagen.FileName);
                //}
                oferta.Imagen = imagen.FileName;
            }
            oferta.Web = web;
            oferta.Codigo = codigo;
            oferta.Precio = precio;
            oferta.IdUsuario = usuario;
            using (HttpClient client = new HttpClient())
            {
                String request = "api/Ofertas/";
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                String json = JsonConvert.SerializeObject(oferta);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                await client.PostAsync(request, content);
            }
        }

        public async Task NewOfertaAsync(int cadena, string titulo, string descripcion,
            String imagen, string web, string codigo, double precio, int usuario, String token)
        {
            Oferta oferta = new Oferta();
            oferta.IdCadena = cadena;
            oferta.Titulo = titulo;
            oferta.Descripcion = descripcion;
            if (imagen != null)
            {
                //String name = await this.uploadService.UploadFileAsycn(imagen, Folders.Images);
                //String name = await this.uploadService.UploadImageBlobAzureAsycn(imagen);
                //using (var memoryStream = new MemoryStream())
                //{
                //    await imagen.CopyToAsync(memoryStream);
                //    bool respuesta = await this.uploadService.UploadFileAWSAsync(memoryStream, imagen.FileName);
                //}
                oferta.Imagen = imagen;
            }
            oferta.Web = web;
            oferta.Codigo = codigo;
            oferta.Precio = precio;
            oferta.IdUsuario = usuario;
            using (HttpClient client = new HttpClient())
            {
                String request = "api/Ofertas/";
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                String json = JsonConvert.SerializeObject(oferta);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                await client.PostAsync(request, content);
            }
        }
        public async Task EditOfertaAsync(int idoferta, int cadena, string titulo, string descripcion,
            IFormFile imagen, string web, string codigo, double precio, int usuario, String token)
        {
            Oferta oferta = new Oferta();
            oferta.Id = idoferta;
            oferta.IdCadena = cadena;
            oferta.Titulo = titulo;
            oferta.Descripcion = descripcion;
            if (imagen != null)
            {
                //String name = await this.uploadService.UploadFileAsycn(imagen, Folders.Images);
                String name = await this.uploadService.UploadImageBlobAzureAsycn(imagen);
                //using (var memoryStream = new MemoryStream())
                //{
                //    await imagen.CopyToAsync(memoryStream);
                //    bool respuesta = await this.uploadService.UploadFileAWSAsync(memoryStream, imagen.FileName);
                //}
                oferta.Imagen = imagen.FileName;
            }
            oferta.Web = web;
            oferta.Codigo = codigo;
            oferta.Precio = precio;
            oferta.IdUsuario = usuario;
            using (HttpClient client = new HttpClient())
            {
                String request = "api/Ofertas/";
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                String json = JsonConvert.SerializeObject(oferta);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                await client.PutAsync(request, content);
            }
        }

        public async Task EditOfertaAsync(int idoferta, int cadena, string titulo, string descripcion,
            String imagen, string web, string codigo, double precio, int usuario, String token)
        {
            Oferta oferta = new Oferta();
            oferta.Id = idoferta;
            oferta.IdCadena = cadena;
            oferta.Titulo = titulo;
            oferta.Descripcion = descripcion;
            if (imagen != null)
            {
                //String name = await this.uploadService.UploadFileAsycn(imagen, Folders.Images);
                //String name = await this.uploadService.UploadImageBlobAzureAsycn(imagen);
                //using (var memoryStream = new MemoryStream())
                //{
                //    await imagen.CopyToAsync(memoryStream);
                //    bool respuesta = await this.uploadService.UploadFileAWSAsync(memoryStream, imagen.FileName);
                //}
                oferta.Imagen = imagen;
            }
            oferta.Web = web;
            oferta.Codigo = codigo;
            oferta.Precio = precio;
            oferta.IdUsuario = usuario;
            using (HttpClient client = new HttpClient())
            {
                String request = "api/Ofertas/";
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                String json = JsonConvert.SerializeObject(oferta);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                await client.PutAsync(request, content);
            }
        }
        public async Task DeleteOfertaAsync(int id, String token)
        {
            using (HttpClient client = new HttpClient())
            {
                String request = "api/Ofertas/" + id;
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                await client.DeleteAsync(request);
            }
        }
        #endregion
        #region USUARIOS
        public async Task<List<Usuario>> GetUsuariosAsync(String token)
        {
            String request = "api/Usuarios";
            return await this.CallApi<List<Usuario>>(request, token);
        }
        public async Task<Usuario> GetUserByIdAsync(int id, String token)
        {
            String request = "api/Usuarios/" + id;
            return await this.CallApi<Usuario>(request, token);
        }
        public async Task<List<Usuario>> GetUsuariosByEmailOrUsernameAsync(String busqueda, String token)
        {
            String request = "api/Usuarios/GetUsuariosByEmailOrUsername/" + busqueda;
            return await this.CallApi<List<Usuario>>(request, token);
        }
        public async Task<Usuario> GetUserbyUsernameAsync(String username)
        {
            String request = "api/Usuarios/GetUserbyUsername/" + username;
            return await this.CallApi<Usuario>(request);
        }
        public async Task<Usuario> GetUserbyEmailAsync(String email)
        {
            String request = "api/Usuarios/GetUserbyEmail/" + email;
            return await this.CallApi<Usuario>(request);
        }
        public async Task<Usuario> LoginAsync(String email, String password)
        {
            //Usuario correcto = await this.GetUserbyEmailAsync(email);
            String request = "api/Usuarios/Login/";
            UsuarioLogin user = new UsuarioLogin();
            user.Email = email;
            user.Password = password;
            //if (correcto != null)
            //{
            //    user.Password = 
            //        CypherService.CifrarContenido(password, correcto.Salt);
            //}
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                String json = JsonConvert.SerializeObject(user);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    //return await response.Content.ReadAsAsync<Usuario>();
                    String data =
                        await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Usuario>(data);
                }
                else
                {
                    return null;
                }
            }
            //return await this.PostApi<Usuario>(request,user);
        }
        public async Task NewUserAsync(string email, String username, string password,
            string nombre, string apellidos, DateTime fechanacimiento)
        {
            String request = "api/Usuarios/NewUser/";
            UsuarioLogin user = new UsuarioLogin();
            //user.Id = this.MaxIdUsuario();
            user.Email = email;
            user.UserName = username;
            //String salt = CypherService.GetSalt();
            //user.Salt = salt;
            //user.ResetSalt = CypherService.GetSalt();
            user.Password = password;
            //user.Password = CypherService.CifrarContenido(password, salt); 
            user.Nombre = nombre;
            user.Apellidos = apellidos;
            //user.FechaAlta = DateTime.Now;
            user.FechaNacimiento = fechanacimiento;
            //Grupo rol = this.GetGrupoByName("user");
            //user.Rol = rol.Id;
            await this.PostApi<UsuarioLogin>(request, user);
        }
        public async Task NewAdminAsync(string email, String username, string password,
            string nombre, string apellidos, DateTime fechanacimiento, String token)
        {
            String request = "api/Usuarios/NewAdmin/";
            UsuarioLogin user = new UsuarioLogin();
            //user.Id = this.MaxIdUsuario();
            user.Email = email;
            user.UserName = username;
            //String salt = CypherService.GetSalt();
            //user.Salt = salt;
            //user.ResetSalt = CypherService.GetSalt();
            //user.Password = CypherService.CifrarContenido(password, salt);
            user.Password = password;
            user.Nombre = nombre;
            user.Apellidos = apellidos;
            //user.FechaAlta = DateTime.Now;
            user.FechaNacimiento = fechanacimiento;
            //Grupo rol = this.GetGrupoByName("user");
            //user.Rol = rol.Id;
            await this.PostApi<UsuarioLogin>(request, user, token);
        }
        public async Task ChangeResetSaltAsync(int iduser, String token)
        {
            String request = "api/Usuarios/ChangeResetSalt/";
            Usuario user = new Usuario();
            user.Id = iduser;
            //user.ResetSalt = CypherService.GetSalt();
            await this.PutApi<Usuario>(request, user, token);
        }
        public async Task EditEmailUserAsync(int iduser, String email, String token)
        {
            String request = "api/Usuarios/EditEmailUser/" + iduser + "/" + email;
            await this.PutApi<Usuario>(request, null, token);
        }
        public async Task EditUsernameUserAsync(int iduser, String username, String token)
        {
            String request = "api/Usuarios/EditUsernameUser/" + iduser + "/" + username;
            await this.PutApi<Usuario>(request, null, token);
        }
        public async Task EditPasswordUserAsync(String username, String password)
        {
            String request = "api/Usuarios/EditPasswordUser/";
            Usuario user = await this.GetUserbyUsernameAsync(username);
            UsuarioLogin usuario = new UsuarioLogin();
            usuario.Id = user.Id;
            usuario.UserName = user.UserName;
            //user.Password = CypherService.CifrarContenido(password, user.Salt);
            usuario.Password = password;
            await this.PutApi<UsuarioLogin>(request, usuario);
        }
        public async Task DeleteUserAsync(int id, String token)
        {
            String request = "api/Usuarios/" + id;
            await this.DeleteApi(request, token);
        }
        #endregion
        #region GRUPOS
        public async Task<Grupo> GetGrupoByIdAsync(int id)
        {
            String request = "api/Grupos/" + id;
            return await this.CallApi<Grupo>(request);
        }
        public async Task<Grupo> GetGrupoByIdAsync(String name)
        {
            String request = "api/Grupos/GetGrupoByName/" + name;
            return await this.CallApi<Grupo>(request);
        }
        #endregion
        #region MAILS
        public async Task SendMailRegisterAsync(Usuario user, String token)
        {
            String request = "api/Mails/SendMailRegister/";
            await this.PostApi<Usuario>(request, user, token);
        }
        public async Task SendMailResetPasswordAsync(Usuario user)
        {
            String request = "api/Mails/SendMailResetPassword/";
            //using (HttpClient client = new HttpClient())
            //{
            //    client.BaseAddress = this.UriApi;
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(this.header);
            //    String json = JsonConvert.SerializeObject(user);
            //    StringContent content =
            //        new StringContent(json, Encoding.UTF8, "application/json");
            //    HttpResponseMessage response =
            //        await client.PostAsync(request, content);
            //}
            await this.PostApi<Usuario>(request, user);
        }
        #endregion
        #region VOTACIONES
        public async Task<int> GetLikesOfertaAsync(int idoferta)
        {
            String request = "api/Votaciones/GetLikesOferta/" + idoferta;
            return await this.CallApi<int>(request);
        }
        public async Task<int> GetDisLikesOfertaAsync(int idoferta)
        {
            String request = "api/Votaciones/GetDisLikesOferta/" + idoferta;
            return await this.CallApi<int>(request);
        }
        public async Task<Votacion> GetVotacionByIdUsuarioAsync(int iduser, int idoferta, String token)
        {
            String request = "api/Votaciones/GetVotacionByIdUsuario/" + iduser + "/" + idoferta;
            return await this.CallApi<Votacion>(request, token);
        }
        public async Task VotarAsync(int iduser, int idoferta, String voto, String token)
        {
            String request = "api/Votaciones/Votar/" + iduser + "/" + idoferta + "/" + voto;
            await this.PostApi<Usuario>(request, null, token);
        }
        #endregion
        #region COMENTARIOS
        public async Task<Comentario> GetComentarioByIdAsync(int id)
        {
            String request = "api/Comentarios/" + id;
            return await this.CallApi<Comentario>(request);
        }
        public async Task<VistaComentariosListApi> GetComentariosPaginadosAsync(int posicion, int idoferta, int pag, String order)
        {
            String request = "api/Comentarios/GetComentariosPaginados/"
                + posicion + "/" + idoferta + "/" + pag + "/" + order;
            return await this.CallApi<VistaComentariosListApi>(request);
        }
        public async Task NewComentarioAsync(int idoferta, int iduser,
            String mensaje, int respuesta, String token)
        {
            Comentario comentario = new Comentario();
            comentario.IdOferta = idoferta;
            comentario.IdUsuario = iduser;
            comentario.Mensaje = mensaje;
            comentario.IdRespuesta = respuesta;
            String request = "api/Comentarios/";
            await this.PostApi<Comentario>(request, comentario, token);
        }
        #endregion
        #region TOKEN
        public async Task<String> GetApiTokenAsync(String email, String password)
        {
            String request = "Auth/Login/";
            LoginModel login = new LoginModel();
            login.Email = email;
            //login.Password = CypherService.CifrarContenido(password, salt);
            login.Password = password;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                String json = JsonConvert.SerializeObject(login);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    String contenido =
                    await response.Content.ReadAsStringAsync();
                    var jObject = JObject.Parse(contenido);
                    return jObject.GetValue("response").ToString();
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion
    }
}
