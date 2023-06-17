using Microsoft.AspNetCore.Mvc;

namespace EvaluationAPI.Controllers
{
    public class TeamController : ControllerBase
    {
        [HttpPost]
        [Route("equalize")]
        public IActionResult equalizeTeam([FromBody] EqualizeRequest request)
        {   
            //Comprobación de que no falten datos
            if (!ModelState.IsValid) return BadRequest(ModelState);
            //Comprobación de datos del arreglo
            if (request.teamSizes.Any(x => x < 0)) return BadRequest("All teamSizes elements must be higher than 0.");

            //Copia de datos de la solicitud
            List<int> sizes = request.teamSizes.ToList<int>();
            int changes = request.changes;

            //Hallar la moda más óptima del arreglo
            int mode;
            List<int> modes = sizes.OrderBy(x => x)
                                .GroupBy(x => x)
                                .OrderByDescending(x => x.Count())
                                .Select(x => x.Key).ToList();
            while (true)
            {
                mode = modes.First();
                modes.RemoveAt(0);
                int avaliableChanges = sizes.Count(x => x > mode);
                if (avaliableChanges >= changes) break;
            } 

            //Cambio de elementos en el arreglo     
            while (changes > 0)
            {
                int max = sizes.Max();
                sizes[sizes.FindIndex(x => x == max)] = mode;
                changes--;
            }
            
            return Ok(sizes.ToArray()); 
        }
    }
}
