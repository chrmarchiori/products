using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Application.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        [
            Required(ErrorMessage = "{0} precisa ser informada.")    
        ]
        public string Descricao { get; set; }
        public DateTime Data_Fabricacao { get; set; }
        public DateTime Data_Validade { get; set; }
        public int Codigo_Fornecedor { get; set; }
        public string Descricao_Fornecedor { get; set; }
        public string CNPJ_Fornecedor { get; set; }
    }
}
