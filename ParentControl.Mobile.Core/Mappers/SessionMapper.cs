using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParentControl.DTO;
using ParentControl.Core.Services.Model;
using Session = ParentControl.Core.Services.Model.Session;

namespace ParentControl.Core.Mappers
{
    public static class SessionMapper
    {
        public static DTO.Session MapToSessionDTO(this Session model)
        {
            return new DTO.Session()
            {
                SessionId = model.SessionId,
                SessionStart = model.SessionStart,
                SessionEnd = model.SessionEnd
            };
        }
    }
}
