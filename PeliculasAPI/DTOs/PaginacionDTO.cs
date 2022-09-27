namespace PeliculasAPI.DTOs
{
    public class PaginacionDTO
    {
        public int Pagina { get; set; } = 1;
        
        
        private int registrosPorPagina = 8;
        private readonly int maximaCantidadDeRegistrosPorPagina = 50;

        public int CantidadRegistrosPorPagina
        {
            get => registrosPorPagina;
            set
            {
                registrosPorPagina = (value > maximaCantidadDeRegistrosPorPagina) ? maximaCantidadDeRegistrosPorPagina : value;
            }
        }


    }
}
