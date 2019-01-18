SequenceReader
---
Načítání read only sequence. High performance načítání bez zbytečných alokací.
* načítání data oddělených koncem řádku
* výsledek je stejný jako bychom volali ReadAllLines
* avšak využitím nového datového typu získáme lepší performance - hodí se zejména pro IoT a čtení sběrnic
* dotnet run
