namespace App.Service.ExceptionHandler;

public class CriticalException(string message) : Exception(message);
