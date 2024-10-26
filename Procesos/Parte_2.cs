using System.Text.Json;

class Agentes{

    public string Cedula { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Clave { get; set; } = string.Empty;

}

class Incidencia
{
    public string Pasaporte { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string WhatsApp { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public double Latitud { get; set; }
    public double Longitud { get; set; }
    public string CodigoAgente { get; set; } = string.Empty;
}

class DatosLogin{
    public string Cedula {get; set;} = string.Empty;
    public string Clave {get; set;} = string.Empty;
}

class Manejador_Agente{

    public static ServerResult Registro_Incidencia(Incidencia incidencia){

        if(!Directory.Exists("incidencias")){
            Directory.CreateDirectory("incidencias");
        }

        var mi_id = Guid.NewGuid().ToString();

        var archivo = $"incidencias/{mi_id}.json";

        var json = JsonSerializer.Serialize(incidencia);

        File.WriteAllText(archivo, json);

        return new ServerResult(true, "Incidencia registrada", mi_id);
    }
    public static ServerResult IniciarSesion(DatosLogin dl){

        var Cedula = dl.Cedula;
        var Clave = dl.Clave;

        if(Cedula.Length != 11){
            return new ServerResult(false, "La cédula debe tener 11 digitos");
        }

        if(Clave.Length == 0){
            return new ServerResult(false, "La clave es obligatoria");
        }

        var archivo = $"agentes/{Cedula}.json";

        if(!File.Exists(archivo)){
            return new ServerResult(false,"Agente no encontrado");
        }

        var json = File.ReadAllText(archivo);

        var Agente = JsonSerializer.Deserialize<Agentes>(json);

        if(Agente.Clave != Clave){
            return new ServerResult(false, "Clave incorrecta");
        }
        Agente.Clave = "***";

        return new ServerResult(true, "Sesión iniciada");
    }
    


    public static ServerResult Registro(Agentes agentes){
        List<string> errores = new List<string>();
        if(agentes.Cedula.Length != 11){

            errores.Add("La cédula debe tener 11 digitos");
        }

        if(agentes.Nombre.Length == 0){
            errores.Add("El nombre es oblgatorio");
        }

        if(errores.Count > 0){
            Console.WriteLine("Errores en el registro:");
            foreach(var error in errores){
                Console.WriteLine(error);
            }
            return new ServerResult(false, "Errores en el registro", errores);
        }

        if(!Directory.Exists("agentes")){
            Directory.CreateDirectory("agentes");
        }

        var archivo = $"agentes/{agentes.Cedula}.json";

        if(File.Exists(archivo)){
            return new ServerResult(false, "El agente ya existe");
        }

        var json = JsonSerializer.Serialize(agentes);

        File.WriteAllText(archivo, json);

        return new ServerResult(true, "Agente registrado");
    }
}