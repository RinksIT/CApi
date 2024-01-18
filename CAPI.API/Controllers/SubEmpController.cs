using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CAPI.CONTRACT;
using CAPI.ENTITIES;
using System.Reflection.Emit;

namespace CAPI.API.Controllers
{
    [Produces("application/json")]
    [Route("api/SubEmp")]
    [ApiController]
    public class SubEmpController : Controller
    {
        private readonly ISubEmp _subEmp;
        public SubEmpController(ISubEmp subEmp)
        {
            _subEmp = subEmp;
        }
        [HttpGet]
        [Route("GetSubEmpList")]
        public List<SubEmpDTO> GetSubEmpList()
        {
            List<SubEmpDTO> lst = new List<SubEmpDTO>();

            try
            {
                lst = _subEmp.GetSubEmpList();
                return lst;
            }
            catch (Exception)
            {
                return lst;
            }
        }

        //Add Person  
        [HttpPost("InsertSubEmp")]
        public bool InsertSubEmp([FromBody] SubEmpDTO subEmpDTO)
        {
            try
            {
                 _subEmp.InsertSubEmp(subEmpDTO);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}


//[10:46 a.m.] LACEY TAVARES
    
//Describe 3 qualities that make you the best candidate for this position and give an example of each.

//Tell us briefly about a development project that you worked on and the approach you took from start to finish?  What was your role on the project?  What technology and design approach did you choose to solve the issue?

//Tell us about a time when you experienced working with a difficult co-worker on a team.  How did you respond, why did you respond this way, what was the outcome?

//Explain what methods and/or steps you use to gather data and develop problem-solving strategies.     


//What are the different HTTP request types supported in Restful Web Services? Can you explain the purpose of each?

//In Durham, we value the uniqueness and diversity of our community. Please describe what steps you would take to work collaboratively with someone who did not share your experiences, beliefs, values, or opinions.

//As a developer what steps do you take to track changes in your code and moving code to a production environment?

//DDSB follows ITIL practices in their day-to-day operations.   Please explain what an Incident and a Problem is and what the difference between the two is.

//When presented with a bug in your coding, what steps would you take to resolve it, how would you escalate the issue if you cannot find a resolution yourself?


//Describe a stressful situation at work and how you handled it.
