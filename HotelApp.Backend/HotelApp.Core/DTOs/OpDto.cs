using System.Linq;
using System.Net;

namespace HotelApp.Core.DTOs
{
    /**
     * Provides context on the result of an operation which mutates the application's state.
     */
    public class OpDto
    {
        #region Factory Methods

        public static OpDto CreateSuccess(object contextObject)
        {
            return new OpDto(true, HttpStatusCode.OK, null, new object[] { contextObject });
        }

        public static OpDto CreateOtherSuccess(HttpStatusCode code, object contextObject)
        {
            return new OpDto(true, code, null, new object[] { contextObject });
        }

        public static OpDto CreateFailure(HttpStatusCode code, string message = null)
        {
            return new OpDto(false, code, message == null ? null : new[] { message });
        }

        public static OpDto CreateInputValidationFailure((string, string)[] validationErrors)
        {
            return new OpDto(false, HttpStatusCode.UnprocessableEntity) { InputValidationErrors = validationErrors };
        }

        public static OpDto CreateInputValidationFailure((string, string) validationError)
        {
            return CreateInputValidationFailure(new[] { validationError });
        }

        public static OpDto CreateStateValidationFailure(string stateFailure)
        {
            return new OpDto(false, HttpStatusCode.UnprocessableEntity) { StateValidationErrors = new[] { stateFailure } };
        }

        #endregion

        public OpDto()
        {
            InputValidationErrors = new (string, string)[0];
            StateValidationErrors = new string[0];
        }

        public OpDto(bool success, HttpStatusCode serverStatusCode = HttpStatusCode.OK, string[] messages = null,
            object[] contextObjects = null, HttpStatusCode? clientStatusCode = null) : this()
        {
            Success = success;
            Messages = messages;
            ClientStatusCode = clientStatusCode;
            ServerStatusCode = serverStatusCode;
            ContextObjects = contextObjects;
        }

        /// <summary>
        /// Flag that determines whether the operation is a success or not.
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// Contains either validation, error, success or other contextual messages.
        /// </summary>
        public string[] Messages { get; private set; }

        /// <summary>
        /// Contains input validation errors encountered within a particular service, such as 'exists', 'unique' and / or other constraints.
        /// </summary>
        public (string, string)[] InputValidationErrors { get; set; }

        /// <summary>
        /// Contains business rules or data integrity validation errors within a particular service.
        /// </summary>
        public string[] StateValidationErrors { get; set; }

        /// <summary>
        /// A suggestion on what status code should be returned by a client server, if any.
        /// </summary>
        public HttpStatusCode? ClientStatusCode { get; set; }

        /// <summary>
        /// Status code to be returned by the API server.
        /// </summary>
        public HttpStatusCode ServerStatusCode { get; }

        /// <summary>
        /// Contains objects that are contextual to the operation result, e.g. the new entity being created.
        /// </summary>
        public object[] ContextObjects { get; }

        public void AddMessage(string message)
        {
            if (Messages != null)
            {
                Messages.Append(message);
            }
            else
            {
                Messages = new string[] { message };
            }
        }
    }
}
