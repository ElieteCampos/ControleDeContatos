using AspNetCoreGeneratedDocument;
using ControleDeContatos.Data;
using ControleDeContatos.Models;


namespace ControleDeContatos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _context;
        public UsuarioRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;
        }
        public UsuarioModel ListarPorId(int id)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Id == id);
        }
        public List<UsuarioModel> BuscarTodos()
        {
            return _context.Usuarios.ToList();
        }
        public UsuarioModel Adicionar(UsuarioModel usuario)
        {            
            usuario.DataCadastro = DateTime.Now;
            //gravar no banco de dados
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return usuario;

        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = ListarPorId(usuario.Id);

            if (usuarioDB == null) throw new Exception("Houve um erro na atualização do usuario!");

            usuarioDB.Nome = usuario.Nome;
            usuarioDB.Email = usuario.Email;
            usuarioDB.Login = usuario.Login;
            usuarioDB.Perfil = usuario.Perfil;
            usuarioDB.DataAtualizacao = DateTime.Now;

            _context.Usuarios.Update(usuarioDB);

            _context.SaveChanges();

            return usuarioDB;
        }

        //apagar/exluir
        public bool Apagar(int id)
        {
            UsuarioModel usuarioDB = ListarPorId(id);
            if (usuarioDB == null) throw new Exception("Houve um erro na exclusão do usuario!");

            _context.Usuarios.Remove(usuarioDB);
            _context.SaveChanges();

            return true;
        }
    }
}
