using BMManager;
using BMManager.BMManagerCD;
using BMManager.BMManagerUI.Funcionarios;
using BMManagerLN.SubEncomendas;
using BMManagerLN.SubFuncionarios;
using BMManagerLN.SubMateriais;
using BMManagerLN.SubMontagens;
using BMManagerLN.SubMoveis;
using BMManagerLN.Exceptions;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Timeouts;

namespace BMManagerLN
{
    public class BMManagerLN : APIBMManagerLN
    {

        //Subsistemas
        private APICSubFuncionarios subFuncionarios;
        private APICSubMontagens subMontagens;
        private APICSubMoveis subMoveis;
        private APICSubMateriais subMateriais;
        private APICSubEncomendas subEncomendas;

        //Construtor
        public BMManagerLN(BMManagerContext db)
        {
            subFuncionarios = new CSubFuncionarios(db);
            subMontagens = new CSubMontagens(db);
            subMoveis = new CSubMoveis(db);
            subMateriais = new CSubMateriais(db);
            subEncomendas = new CSubEncomendas(db);
        }

        //Métodos da lógica de negócio

        //Métodos SubFuncionários
        public Task<List<Funcionario>> GetFuncionarios()
        {
            return subFuncionarios.GetFuncionarios();
        }

        public Task<Funcionario> GetFuncionario(int codFuncionario)
        {
            return subFuncionarios.GetFuncionario(codFuncionario);
        }

        public Task PutFuncionario(Funcionario funcionario)
        {
            return subFuncionarios.PutFuncionario(funcionario);
        }

        public Task<FuncionarioDTO?> AutenticarUtilizador(string codigo, string senha)
        {
            return subFuncionarios.AutenticarUtilizador(codigo, senha);
        }

        public async Task<List<Funcionario>> FuncionariosParticipamMontagem(int codMontagem)
        {
            return await subFuncionarios.FuncionariosParticipamMontagem(codMontagem);
        }

        //Métodos SubMontagens
        public Task<List<Montagem>> GetMontagens()
        {
            return subMontagens.GetMontagens();
        }

        public Task<Montagem> GetMontagem(int codMontagem)
        {
            return subMontagens.GetMontagem(codMontagem);
        }

        public async Task NovaMontagem(int codMovel, int codFuncionario)
        {
            Movel movel = await subMoveis.GetMovel(codMovel);

            bool materiaisSuficientes = true; //alterar para método que verifica
            //verificar se existem materiais suficientes para primeira etapa ou todas
            if (!materiaisSuficientes)
            {
                throw new MateriaisInsuficientesException("MateriaisInsoficientes");
            }

            Montagem montagem = new Montagem();
            montagem.Movel = codMovel;
            montagem.Etapa = movel.Etapas_Montagem[1];
            montagem.Data_Inicial = DateTime.Now;

            await subMontagens.PutMontagem(montagem);
        }

        public async Task<Dictionary<Material, int>> MateriaisUtilizadosMontagem(int codMontagem)
        {
            Montagem montagem = await subMontagens.GetMontagem(codMontagem);
            Etapa etapaAtual = await subMoveis.GetEtapa(montagem.Etapa);
            Func<Etapa, bool> condicao;
            if (montagem.Etapa_Concluida) condicao = e => e.Movel == codMontagem & e.Numero < etapaAtual.Numero;
            else condicao = e => e.Movel == codMontagem & e.Numero <= etapaAtual.Numero;
            Dictionary<int, Etapa> etapas = await subMoveis.GetEtapasMovelCondicao(condicao);
            int[] codEtapas = etapas.Values.Select(e => e.Codigo_Etapa).ToArray();
            return await subMateriais.GetMateriaisEtapas(codEtapas);
        }

        public async Task<Dictionary<Material, int>> MateriaisPorUtilizarMontagem(int codMontagem)
        {
            Montagem montagem = await subMontagens.GetMontagem(codMontagem);
            Etapa etapaAtual = await subMoveis.GetEtapa(montagem.Etapa);
            Func<Etapa, bool> condicao;
            if (montagem.Etapa_Concluida) condicao = e => e.Movel == codMontagem & e.Numero > etapaAtual.Numero;
            else condicao = e => e.Movel == codMontagem & e.Numero >= etapaAtual.Numero;
            Dictionary<int, Etapa> etapas = await subMoveis.GetEtapasMovelCondicao(condicao);
            int[] codEtapas = etapas.Values.Select(e => e.Codigo_Etapa).ToArray();
            return await subMateriais.GetMateriaisEtapas(codEtapas);
        }


        //Métodos SubMoveis
        public Task<List<Movel>> GetMoveis()
        {
            return subMoveis.GetMoveis();
        }

        public Task<Movel> GetMovel(int codMovel)
        {
            return subMoveis.GetMovel(codMovel);
        }

        public Task PutMovel(Movel movel)
        {
            return subMoveis.PutMovel(movel);
        }

        public Task<List<Etapa>> GetEtapas()
        {
            return subMoveis.GetEtapas();
        }
        public Task<Etapa> GetEtapa(int codEtapa)
        {
            return subMoveis.GetEtapa(codEtapa);
        }

        public Task<Dictionary<int,Etapa>> GetEtapasMovel(int codMovel)
        {
            return subMoveis.GetEtapasMovel(codMovel);
        }

        public Task PutEtapa(Etapa etapa)
        {
            return subMoveis.PutEtapa(etapa);
        }

        //Métodos SubMateriais
        public Task<List<Material>> GetMateriais()
        {
            return subMateriais.GetMateriais();
        }

        public Task<Material> GetMaterial(int codMaterial)
        {
            return subMateriais.GetMaterial(codMaterial);
        }

        public Task PutMaterial(Material material)
        {
            return subMateriais.PutMaterial(material);
        }

        //Métodos SubEncomendas
        public Task<List<Encomenda>> GetEncomendas()
        {
            return subEncomendas.GetEncomendas();
        }
        public Task<Encomenda> GetEncomenda(int codEncomenda)
        {
            return subEncomendas.GetEncomenda(codEncomenda);
        }
        public Task PutEncomenda(Encomenda encomenda)
        {
            return subEncomendas.PutEncomenda(encomenda);
        }

        //Métodos Auxiliares
        public string GetDescricao(Enum valor)
        {
            var campo = valor.GetType().GetField(valor.ToString());
            var descricao = (DescriptionAttribute)Attribute.GetCustomAttribute(campo, typeof(DescriptionAttribute));
            return descricao != null ? descricao.Description : valor.ToString();
        }

        public string ByteArrayParaImagem(byte[] bytes)
        {
            string tipo;
            if (bytes[0] == 0xFF && bytes[1] == 0xD8 && bytes[2] == 0xFF) tipo = "image/jpeg";
            else if (bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47) tipo = "image/png";
            else if (bytes[0] == 0x47 && bytes[1] == 0x49 && bytes[2] == 0x46) tipo = "image/gif";
            else if (bytes[0] == 0x42 && bytes[1] == 0x4D) tipo = "image/bmp";
            else tipo = "image/webp";
            return $"data:{tipo};base64,{Convert.ToBase64String(bytes)}";
        }
    }
}
