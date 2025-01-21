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
using System.Collections.Generic;
using Microsoft.Extensions.Internal;

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

        public Task ModificarFuncionario (int codFuncionario, string nome, string email, string senha, string telefone, Equipa? equipa, bool conta_ativa) {
            return subFuncionarios.ModificarFuncionario(codFuncionario, nome, email, senha, telefone, equipa, conta_ativa);
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

        public async Task<Montagem> NovaMontagem(int codMovel, int codFuncionario)
        {
            Movel movel = await subMoveis.GetMovel(codMovel);
            Func<Etapa, bool> condicao = e => e.Movel == codMovel & e.Numero == 1;
            Dictionary<int, Etapa> etapas = await subMoveis.GetEtapasMovelCondicao(condicao);

            Montagem montagem = new Montagem();
            montagem.Movel = codMovel;
            montagem.Etapa = etapas[1].Codigo_Etapa;
            montagem.Etapa_Concluida = false;
            montagem.Data_Inicial = DateTime.Now;

            await subMontagens.PutMontagem(montagem);

            await subFuncionarios.AssociaFuncionarioMontagem(codFuncionario, montagem.Numero);

            return montagem;
        }

        public async Task<(int,int)> MateriaisSuficientesMontagem(int codMovel)
        {
            Movel movel = await subMoveis.GetMovel(codMovel);

            Dictionary<int, Etapa> etapas = await subMoveis.GetEtapasMovel(codMovel);
            bool materiaisSuficientes = true;
            int etapaComMateriais = 0;
            for (int i = 1; i <= etapas.Count && materiaisSuficientes; i++)
            {
                Dictionary<Material, int> materiasEtapa = await subMateriais.GetMateriaisEtapa(etapas[i].Codigo_Etapa);
                materiaisSuficientes = MateriaisSuficientes(materiasEtapa);
                if (materiaisSuficientes)
                {
                    etapaComMateriais++;
                }
            }
            return (etapaComMateriais, etapas.Count);
        }

        private bool MateriaisSuficientes(Dictionary<Material, int> materiais)
        {
            foreach (KeyValuePair<Material, int> materiaisQuantidades in materiais)
            {
                if (materiaisQuantidades.Key.Quantidade < materiaisQuantidades.Value)
                {
                    return false;
                }
            }
            return true;
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

        public async Task<string> AdicionarMontagensEncomenda(int codEncomenda, List<int> montagens)
        {
            Dictionary<Movel, int> moveisQueFaltam;
            moveisQueFaltam = await GetMoveisQueFaltamEncomenda(codEncomenda);
            string erro = "";
            foreach (int id in montagens)
            {
                Montagem mont;
                mont = await subMontagens.GetMontagem(id);
                Movel movel;
                movel = await subMoveis.GetMovel(mont.Movel);
                if (moveisQueFaltam.ContainsKey(movel))
                {
                    if (moveisQueFaltam[movel] > 1)
                    {
                        moveisQueFaltam[movel] = moveisQueFaltam[movel]--;
                        await subMontagens.AssociarAEncomenda(id, codEncomenda);
                    }
                    else
                    {
                        moveisQueFaltam.Remove(movel);
                        await subMontagens.AssociarAEncomenda(id, codEncomenda);
                    }
                }
                else
                {
                    erro = "Não é possível adicionar montagens que não sejam necessárias na encomenda, uma ou mais montagens não foram adicionadas";
                }
            
            }
            if(moveisQueFaltam.Count == 0)
            {
                await subEncomendas.ConcluirEncomenda(codEncomenda);

            }
            return erro;
        }

        public async Task<List<(int, string)>> GetMontagensNecessarias(int codEncomenda)
        {
            List<(int, string)> montagens = new List<(int, string)>();
            List<Movel> moveisNecessarios;
            Dictionary<Movel, int> res;
            res = await GetMoveisQueFaltamEncomenda(codEncomenda);
            moveisNecessarios = res.Keys.ToList();
            List<Montagem> m;
            m = await subMontagens.GetMontagens();
            foreach (Montagem mont in m)
            {
                if(mont.Encomenda==null && mont.Estado.Equals(Estado.Concluida))
                {
                    Movel mov = moveisNecessarios.Find(x => x.Numero == mont.Movel);
                    if (mov != null)
                    {
                        montagens.Add((mont.Numero, mov.Nome));
                    }
                }
            }
            return montagens;
        }

        public async Task<List<(int, string)>> GetMontagensEncomenda(int codEncomenda)
        {
            List<Montagem> montagens;
            montagens = await subMontagens.GetMontagens();
            List<(int, string)> res = new List<(int, string)>();
            foreach (Montagem montagem in montagens)
            {
                if(montagem.Encomenda!=null && montagem.Encomenda == codEncomenda)
                {
                    Movel movel;
                    movel = await subMoveis.GetMovel(montagem.Movel);
                    res.Add((montagem.Numero, movel.Nome));
                }
            }
            return res;
        }

        //Métodos SubMoveis
        public Task<List<Movel>> GetMoveis()
        {
            return subMoveis.GetMoveis();
        }

        public Task<List<Encomenda_Precisa_Movel>> GetEncomendaPrecisaMovel() {
            return subMoveis.GetEncomendaPrecisaMovel();
        }

        public Task<Movel> GetMovel(int codMovel)
        {
            return subMoveis.GetMovel(codMovel);
        }

        public bool MovelExiste(int codMovel)
        {
            return subMoveis.MovelExiste(codMovel);
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

        public Task<Dictionary<Movel,int>> GetMoveisEncomenda(int codEncomenda)
        {
            return subMoveis.GetMoveisEncomenda(codEncomenda);
        }
        public async Task<Dictionary<Movel, int>> GetMoveisQueFaltamEncomenda(int codEncomenda)
        {
            Dictionary<Movel, int> moveisEncomenda;
            moveisEncomenda = await subMoveis.GetMoveisEncomenda(codEncomenda);
            List<(int, string)> montagensAssociadas;
            montagensAssociadas = await GetMontagensEncomenda(codEncomenda);
            foreach((int mont, string nome) in montagensAssociadas)
            {
                Montagem montagem;
                montagem = await GetMontagem(mont);
                int idMovel = montagem.Movel;
                Movel movel;
                movel = await GetMovel(idMovel);
                if (moveisEncomenda[movel] > 1)
                {
                    moveisEncomenda[movel] = moveisEncomenda[movel]--;
                }
                else
                {
                    moveisEncomenda.Remove(movel);
                }
            }
            return moveisEncomenda;
        }


        public Task AdicionaMovelEncomenda(int codMovel, int quantidade, int codEncomenda)
        {
            return subMoveis.AdicionaMovelEncomenda(codMovel, quantidade, codEncomenda);
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

        public Task<Dictionary<Material, int>> GetMateriaisEtapa(int codEtapa)
        {
            return subMateriais.GetMateriaisEtapa(codEtapa);
        }

        public Task AdicionaMaterialEtapa(int codMaterial, int quantidade, int codEtapa)
        {
            return subMateriais.AdicionaMaterialEtapa(codMaterial, quantidade, codEtapa);
        }

        public Task AlterarQuantidadeMaterial(int codMaterial, int novaQuantidade)
        {
            return subMateriais.AlterarQuantidadeMaterial(codMaterial, novaQuantidade);
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
