using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace web.api.demarcacao.gestao.usuarios.Endpoint.Models
{
    public class TerrenoVM
    {
        [Required(AllowEmptyStrings = true)]
        public long IdEmpreendimento { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string Descricao { get; set; }
        public IEnumerable<CoordenadasVM> Coordenadas { get; set; }
    }

    public class TerrenoResponseVM : TerrenoVM
    {
        public long Id { get; set; }
    }

    public class CoordenadasVM
    {
        public long Id { get; set; }

        [Required(AllowEmptyStrings = true)]
        public int Ordem { get; set; }

        [Required(AllowEmptyStrings = true)]
        public decimal Longitude { get; set; }

        [Required(AllowEmptyStrings = true)]
        public decimal Latitude { get; set; }
    }
}
