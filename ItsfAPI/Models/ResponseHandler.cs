namespace ItsfAPI.Models;

public class ResponseHandler
{
    public static ApiResponse GetAppResponse(ResponseType type, object? contract)
    {
        ApiResponse response;

        response = new ApiResponse { ResponseData = contract };

        switch (type)
        {
            case ResponseType.Success:
                response.Code = "200";
                response.Message = "Success";
                break;
            case ResponseType.NotFound:
                response.Code = "404";
                response.Message = "No record available";
                break;
        }

        return response;
    }
}