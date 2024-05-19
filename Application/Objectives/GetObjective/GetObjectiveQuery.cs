using Application.Abstraction.Messaging;
using Application.Models;
using Application.Objectives.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Objectives.GetObjective
{
    public record GetObjectiveQuery(Guid objectiveId, Guid userId, string userRole) : IQuery<GetObjectiveResponseDto>
    {
    }
}
