using System;

namespace Products.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public DateTime Data_Fabricacao { get; set; }
        public DateTime Data_Validade { get; set; }
        public int Codigo_Fornecedor { get; set; }
        public string Descricao_Fornecedor { get; set; }
        public string CNPJ_Fornecedor { get; set; }
    }
}
