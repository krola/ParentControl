using ParentControl.DTO;

namespace ParentControl.Infrastructure.Mappers
{
    public static class SessionMapper
    {
        public static CreateSessionParams MapToCreateParameters(this Session model)
        {
            return new CreateSessionParams()
            {
                Id = model.Id,
                SessionStart = model.SessionStart,
                SessionEnd = model.SessionEnd
            };
        }

        public static Session MapToSession(this CreateSessionParams model)
        {
            return new Session()
            {
                Id = model.Id,
                SessionStart = model.SessionStart,
                SessionEnd = model.SessionEnd
            };
        }

        public static UpdateSessionParams MapToUpdateParameters(this Session model)
        {
            return new UpdateSessionParams()
            {
                Id = model.Id,
                SessionStart = model.SessionStart,
                SessionEnd = model.SessionEnd
            };
        }
    }
}
