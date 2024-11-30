using System;

namespace StudentPortal.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
//RequestId:

//Type: string? (nullable string)
//Purpose: Stores the unique identifier for the request that caused the error. This can be useful for logging and troubleshooting specific issues.
//ShowRequestId:

//Type: bool(read - only property)
//Purpose: Indicates whether the RequestId should be displayed. It returns true if RequestId is not null or empty, otherwise false.
//How It Fits In
//Error Handling: When an error occurs, the application can generate a RequestId to track the error. This ID can be logged and displayed to the user, helping both developers and users to reference the specific error instance.
//User Feedback: By showing the RequestId, users can provide this ID when reporting issues, making it easier for developers to find the corresponding logs and diagnose the problem.