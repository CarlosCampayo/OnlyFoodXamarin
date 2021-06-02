using OnlyFoodXamarin.Models;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlyFoodXamarin.Repositories
{
    public class RepositoryRealm
    {
        private Realm realmConnection;
        public RepositoryRealm()
        {
            this.realmConnection = Realm.GetInstance();
        }
        public List<UsuarioLoginRealm> GetUsuarios()
        {
            List<UsuarioLoginRealm> usuarios = this.realmConnection.All<UsuarioLoginRealm>().ToList();
            return usuarios;
        }
        public UsuarioLoginRealm FindUsuario(int id)
        {
            return this.realmConnection.All<UsuarioLoginRealm>()
                .FirstOrDefault(z => z.Id == id);
        }
        public UsuarioLoginRealm GetUsuarioLogin()
        {
            return this.realmConnection.All<UsuarioLoginRealm>()
                .FirstOrDefault();
        }
        public void InsertarUsuario(int id,String email,String password, int rol)
        {
            using (Transaction transaction = this.realmConnection.BeginWrite())
            {
                UsuarioLoginRealm usuario = new UsuarioLoginRealm();
                usuario.Id = id;
                usuario.Email = email;
                usuario.Password = password;
                usuario.Rol = rol;
                this.realmConnection.Add(usuario);
                transaction.Commit();
            }
        }
        public void UpdateUsuario(int id, String email, String password)
        {
            UsuarioLoginRealm usuario = this.FindUsuario(id);
            using (Transaction transaction = this.realmConnection.BeginWrite())
            {
                usuario.Email = email;
                usuario.Password = password;
                transaction.Commit();
            }
        }
        public void DeleteUsuario(int id)
        {
            UsuarioLoginRealm usuario = this.FindUsuario(id);
            using (Transaction transaction = this.realmConnection.BeginWrite())
            {
                this.realmConnection.Remove(usuario);
                transaction.Commit();
            }
        }
    }
}
