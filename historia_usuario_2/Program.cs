using historia_usuario_2.Models;

List<Paciente> pacientes = new()
{
    new Paciente
    {
        Id = 1, Nombre = "Juan", Edad = 28, Sintoma = "Fiebre", Telefono = "3001112222",
        Mascotas = new List<Mascota>
        {
            new Mascota { Id = 1, IdDueno = 1, Nombre = "Coco",  Especie = "Perro", Raza = "Bulldog",  Edad = 3 },
            new Mascota { Id = 2, IdDueno = 1, Nombre = "Luna",  Especie = "Gato",  Raza = "Siamés",   Edad = 2 }
        }
    },
    new Paciente
    {
        Id = 2, Nombre = "María", Edad = 34, Sintoma = "Tos", Telefono = "3012223333",
        Mascotas = new List<Mascota>
        {
            new Mascota { Id = 3, IdDueno = 2, Nombre = "Rocky", Especie = "Perro", Raza = "Labrador", Edad = 5 }
        }
    },
    new Paciente
    {
        Id = 3, Nombre = "Carlos", Edad = 19, Sintoma = "Vómito", Telefono = "3023334444",
        Mascotas = new List<Mascota>
        {
            new Mascota { Id = 4, IdDueno = 3, Nombre = "Pico",  Especie = "Ave",   Raza = "",         Edad = 1 },
            new Mascota { Id = 5, IdDueno = 3, Nombre = "Nemo",  Especie = "Pez",   Raza = "Payaso",   Edad = 1 }
        }
    },
    new Paciente
    {
        Id = 4, Nombre = "Ana", Edad = 45, Sintoma = "Decaimiento", Telefono = "3034445555",
        Mascotas = new List<Mascota>
        {
            new Mascota { Id = 6, IdDueno = 4, Nombre = "Mia",   Especie = "Gato",  Raza = "Persa",    Edad = 4 }
        }
    },
    new Paciente
    {
        Id = 5, Nombre = "Sofía", Edad = 22, Sintoma = "Diarrea", Telefono = "3045556666",
        Mascotas = new List<Mascota>
        {
            new Mascota { Id = 7, IdDueno = 5, Nombre = "Max",   Especie = "Perro", Raza = "Poodle",   Edad = 6 },
            new Mascota { Id = 8, IdDueno = 5, Nombre = "Oreo",  Especie = "Perro", Raza = "",         Edad = 2 }
        }
    },
    new Paciente
    {
        Id = 6, Nombre = "Diego", Edad = 31, Sintoma = "Alergia", Telefono = "3056667777",
        Mascotas = new List<Mascota>
        {
            new Mascota { Id = 9, IdDueno = 6, Nombre = "Thor",  Especie = "Perro", Raza = "Pastor",   Edad = 3 }
        }
    },
};

Dictionary<int, Paciente> pacientesPorId = pacientes.ToDictionary(p => p.Id);

pacientes.Add(new Paciente
{
    Id = 7, Nombre = "Valentina", Edad = 17, Sintoma = "Pérdida de apetito", Telefono = "3067778888",
    Mascotas = new List<Mascota>
    {
        new Mascota { Id = 10, IdDueno = 7, Nombre = "Kira", Especie = "Perro", Raza = "Husky", Edad = 1 }
    }
});
pacientesPorId[7] = pacientes.Last(); // mantener el diccionario sincronizado

pacientesPorId[1].Sintoma = "Fiebre alta";

var aEliminar = pacientes.FirstOrDefault(p => p.Id == 7);
if (aEliminar != null)
{
    pacientes.Remove(aEliminar);
    pacientesPorId.Remove(7);
}

Console.WriteLine("── Acceso por ID ──────────────────────────────");
if (pacientesPorId.TryGetValue(3, out var encontrado))
    Console.WriteLine($"Paciente ID 3: {encontrado.Nombre} | Síntoma: {encontrado.Sintoma}");

Console.WriteLine();


Console.WriteLine("── Where: pacientes mayores de 25 ─────────────");

var mayoresDe25Metodo = pacientes.Where(p => p.Edad > 25);

var mayoresDe25Consulta = from p in pacientes
                          where p.Edad > 25
                          select p;

foreach (var p in mayoresDe25Metodo)
    Console.WriteLine($"  {p.Nombre} ({p.Edad})");

Console.WriteLine();
Console.WriteLine("── Select: proyectar solo nombres ─────────────");

var nombres = pacientes.Select(p => p.Nombre.ToUpper());
foreach (var n in nombres)
    Console.WriteLine($"  {n}");

Console.WriteLine();
Console.WriteLine("── OrderBy / OrderByDescending ─────────────────");

var porEdadAsc  = pacientes.OrderBy(p => p.Edad).Select(p => $"{p.Nombre} ({p.Edad})");
var porEdadDesc = pacientes.OrderByDescending(p => p.Edad).Select(p => $"{p.Nombre} ({p.Edad})");

Console.WriteLine("  Ascendente:  " + string.Join(", ", porEdadAsc));
Console.WriteLine("  Descendente: " + string.Join(", ", porEdadDesc));

Console.WriteLine();
Console.WriteLine("── GroupBy: pacientes por especie de sus mascotas ──");

var porEspecie = pacientes
    .SelectMany(p => p.Mascotas, (p, m) => new { Paciente = p, Mascota = m })
    .GroupBy(x => x.Mascota.Especie);

foreach (var grupo in porEspecie)
{
    Console.WriteLine($"  [{grupo.Key}]");
    foreach (var x in grupo)
        Console.WriteLine($"    {x.Paciente.Nombre} → {x.Mascota.Nombre}");
}

Console.WriteLine();
Console.WriteLine("── First / FirstOrDefault / Any / All / Count ──");

var primerPerro = pacientes.FirstOrDefault(p => p.Mascotas.Any(m => m.Especie == "Perro"));
Console.WriteLine($"  Primer dueño de perro: {primerPerro?.Nombre ?? "ninguno"}");

bool hayConGato  = pacientes.Any(p => p.Mascotas.Any(m => m.Especie == "Gato"));
bool todosTienenMascota = pacientes.All(p => p.Mascotas.Count > 0);
int totalPacientes = pacientes.Count();

Console.WriteLine($"  ¿Hay paciente con gato?      {hayConGato}");
Console.WriteLine($"  ¿Todos tienen mascota?       {todosTienenMascota}");
Console.WriteLine($"  Total de pacientes:          {totalPacientes}");

Console.WriteLine();


Console.WriteLine("── Consulta encadenada: dueños de Perro, ordenados por edad ──");

var dueniosPerro = pacientes
    .Where(p => p.Mascotas.Any(m => m.Especie == "Perro"))   // solo dueños de perros
    .OrderBy(p => p.Edad)                                     // ordenar por edad del paciente
    .Select(p => new                                          // proyectar solo lo necesario
    {
        p.Nombre,
        p.Telefono,
        Mascotas = p.Mascotas.Where(m => m.Especie == "Perro").Select(m => m.Nombre)
    });

foreach (var d in dueniosPerro)
    Console.WriteLine($"  {d.Nombre} | Tel: {d.Telefono} | Perros: {string.Join(", ", d.Mascotas)}");

Console.WriteLine();

Console.WriteLine("── Paciente más joven y más viejo ─────────────");

var masJoven = pacientes.MinBy(p => p.Edad);
var masViejo = pacientes.MaxBy(p => p.Edad);
Console.WriteLine($"  Más joven: {masJoven?.Nombre} ({masJoven?.Edad} años)");
Console.WriteLine($"  Más viejo: {masViejo?.Nombre} ({masViejo?.Edad} años)");

Console.WriteLine();
Console.WriteLine("── Conteo de mascotas por especie ─────────────");

var conteoEspecie = pacientes
    .SelectMany(p => p.Mascotas)
    .GroupBy(m => m.Especie)
    .Select(g => new { Especie = g.Key, Total = g.Count() })
    .OrderByDescending(x => x.Total);

foreach (var e in conteoEspecie)
    Console.WriteLine($"  {e.Especie}: {e.Total}");

Console.WriteLine();
Console.WriteLine("── ¿Existe mascota sin raza definida? ─────────");

bool sinRaza = pacientes.SelectMany(p => p.Mascotas).Any(m => string.IsNullOrWhiteSpace(m.Raza));
Console.WriteLine($"  {sinRaza}");

Console.WriteLine();
Console.WriteLine("── Nombres de pacientes en mayúsculas, alfabético ──");

var nombresOrdenados = pacientes
    .Select(p => p.Nombre.ToUpper())
    .OrderBy(n => n);

foreach (var n in nombresOrdenados)
    Console.WriteLine($"  {n}");
