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
            int[] sizes = request.teamSizes;
            int k = request.changes;

            //Hallar la moda del arreglo
            sizes = sizes.OrderBy(x => x).ToArray();
            int mode = sizes.GroupBy(x => x)
                        .OrderByDescending(x => x.Count())
                        .Select(x => x.Key)
                        .First();

            //Cambio de elementos en el arreglo
            for (int i = 1; k > 0; i++)
            {
                if (sizes[sizes.Length - i] <= mode) break;
                sizes[sizes.Length - i] = mode;
                k--;
            }
            //Print en consola del arreglo cambiado
            Console.WriteLine(String.Join(",", sizes));
            //Máximo de grupos cambiados
            int count = sizes.Count(x => x == mode);
            return Ok(count); 
        }
    }
}
