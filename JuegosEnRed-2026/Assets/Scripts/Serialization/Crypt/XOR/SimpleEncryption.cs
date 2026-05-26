using System.Text;

public static class SimpleEncryption
{
    // Nuestra clave secreta. ¡Nadie fuera del equipo de desarrollo debe saberla!
    private static char claveSecreta = 'K'; 

    // Al ser XOR, EL MISMO MÉTODO ENCRIPTA Y DESENCRIPTA
    public static string ProcesarXOR(string datosTexto)
    {
        // Usamos StringBuilder por rendimiento, para no generar basura en memoria
        StringBuilder resultado = new StringBuilder();

        // Recorremos cada carácter del texto plano o JSON
        for (int i = 0; i < datosTexto.Length; i++)
        {
            // Aplicamos el operador XOR (el símbolo ^ en C#) entre el caracter y la clave
            char caracterCifrado = (char)(datosTexto[i] ^ claveSecreta);
            
            // Lo añadimos al resultado
            resultado.Append(caracterCifrado);
        }

        return resultado.ToString();
    }
}