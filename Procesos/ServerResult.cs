public class ServerResult{
    public bool Exito {get; set;}
    public string Mensaje {get; set;}
    public object Datos {get; set;}

    public ServerResult(bool exito, string mensaje, object datos){
        Exito = exito;
        Mensaje = mensaje;
        Datos = datos;
    }

    public ServerResult(bool Exito, string mensaje){
        Exito = true;
        Mensaje = mensaje;
    }

    public ServerResult(bool Exito){
        Exito = true;
    }

    public ServerResult(){
        Exito = true;
    }
}


