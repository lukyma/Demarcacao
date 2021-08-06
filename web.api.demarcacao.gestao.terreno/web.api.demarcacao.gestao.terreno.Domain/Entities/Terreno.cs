using System;
using System.Collections.Generic;
using System.Linq;
using web.api.demarcacao.gestao.terreno.Domain.Entities.Core;

namespace web.api.demarcacao.gestao.terreno.Domain.Entities
{
    public class Terreno : AggregateRoot<long>
    {
        public Terreno()
        {
            Coordenadas = new HashSet<Coordenada>();
        }
        private const double RaioTerra = 6371e3;
        public long IdEmpreendimento { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<Coordenada> Coordenadas { get; set; }
        public (double valor, string valorFormatado) SomaDistanciaPontos => SomarDistanciaEntrePontos();
        public (double valor, string valorFormatado) AreaTotal => CalcularAreaPoligonos();
        private (double valor, string valorFormatado) SomarDistanciaEntrePontos()
        {
            if (Coordenadas.Count < 2)
            {
                return (0, "Só é possível somar distancia entre um ou mais coordenadas.");
            }
            var coordenadasOrdenadas = Coordenadas.OrderBy(o => o.Ordem).ToList();
            double distancia = 0;
            for (int i = 0; i < coordenadasOrdenadas.Count; i++)
            {
                Coordenada coordenadas1 = coordenadasOrdenadas.ElementAt(i);
                Coordenada coordenadas2;

                if (i + 1 < coordenadasOrdenadas.Count)
                {
                    coordenadas2 = coordenadasOrdenadas.ElementAt(i + 1);
                }
                else
                {
                    coordenadas2 = coordenadasOrdenadas.ElementAt(0);
                }

                distancia += CalcularDistancia(coordenadas1.Latitude, coordenadas1.Longitude, coordenadas2.Latitude, coordenadas2.Longitude);
            }
            distancia = distancia < 0 ? distancia * -1 : distancia;
            return (distancia, distancia.ToString("0.00 metros"));
        }

        private (double, string) CalcularAreaPoligonos()
        {
            double area = 0;
            if (Coordenadas.Count < 3)
            {
                return (0, "Só é possível calcular a área com 3 ou mais coordenadas.");
            }

            var coordenadasOrdenadas = Coordenadas.OrderBy(o => o.Ordem).ToList();

            for (int i = 0; i < coordenadasOrdenadas.Count; i++)
            {
                Coordenada coordenadas1 = coordenadasOrdenadas.ElementAt(i);
                Coordenada coordenadas2;

                if (i + 1 < Coordenadas.Count)
                {
                    coordenadas2 = coordenadasOrdenadas.ElementAt(i + 1);
                }
                else
                {
                    coordenadas2 = coordenadasOrdenadas.ElementAt(0);
                }

                var lon1XRad = Convert.ToDouble(coordenadas1.Longitude) * Math.PI / 180;
                var lon2XRad = Convert.ToDouble(coordenadas2.Longitude) * Math.PI / 180;
                var lat1YRad = Convert.ToDouble(coordenadas1.Latitude) * Math.PI / 180;
                var lat2YRad = Convert.ToDouble(coordenadas2.Latitude) * Math.PI / 180;

                area += (lon2XRad - lon1XRad) * (2 + Math.Sin(lat1YRad) + Math.Sin(lat2YRad));
            }

            double result = area * RaioTerra * RaioTerra / 2.0;
            result = result < 0 ? result * -1 : result;
            return (result, result.ToString("0.00 m2"));
        }

        private double CalcularDistancia(decimal latitude1, decimal longitude1, decimal latitude2, decimal longitude2)
        {
            double r = RaioTerra;
            double lat1 = Convert.ToDouble(latitude1);
            double lon1 = Convert.ToDouble(longitude1);
            double lat2 = Convert.ToDouble(latitude2);
            double lon2 = Convert.ToDouble(longitude2);

            double latPi1 = lat1 * Math.PI / 180;
            double latPi2 = lat2 * Math.PI / 180;

            double diflat = (lat2 - lat1) * Math.PI / 180;
            double diflon = (lon2 - lon1) * Math.PI / 180;

            double a = Math.Sin(diflat / 2) * Math.Sin(diflat / 2) +
                        Math.Cos(latPi1) * Math.Cos(latPi2) *
                        Math.Sin(diflon / 2) * Math.Sin(diflon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double d = r * c;

            return d;
        }
    }
}
