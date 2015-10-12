#CQRS Demo

##Inhalt
Diese Demo Applikation besteht aus zwei branches. Der `master` branch enthält eine leere Solution, 
mit der das vorliegende Beispiel erstellt werden kann.
Der zweite branch namens `complete-solution` steht erst nach der ADC 15 zur Verfügung, damit keiner schummelt ;-)

Die Lösung wird mit Hilfe des **fluent-CQRS** Frameworks entwickelt.

Das Verwalten der externen Libraries erfolgt mit Hilfe des Paketierungs Managers [Paket](http://fsprojects.github.io/Paket/).
Um sich die Arbeit zu erleichtern, sollte man sich die Paket Extension für Visual Studio installieren. Diese erkennt
die `paket.dependencies` Datei, mit der man weitere gewünschte Packages einbinden kann.

##Setup
Nach dem Öffenen der Solution und dem Installieren der Paket Extensions, bitte mit einem Rechtsklick auf die 
`paket.dependencies` Datei die Installation der Pakete starten.

~Fertig

##Aufgabe
Es soll eine Applikation gebaut werden, mit der man einen Mietvertrag vorbereiten kann und bei Einzug des
Mieters wird diese Vorbereitung abgeschlossen.

###Mietvertrag vorbereiten
Wenn ein Mieter in Verhandlung mit einem Vermiter steht, kann die Wohnung während dieses Zeitraumes reserviert werden.
Unter Angabe der Adressdaten, einer Einheitennummer und den Mieterdaten sowie dem geplanten Einzugstermin kann
eine Reservierung und Vorbereitung durchgeführt werden.

###Einzug
Wenn eine Reservierung vorligt und die Vorbereitung durchgeführt wurde, kann der Einzug im System gespeichert werden. 
Dazu muss zusätzlich der tatsächliche Einzugstermin angegeben werden.

Liegt noch keine Reservierung bzw. Vorbereitung vor, wird ein Business Fehler (`Fault`) geworfen und ausgegeben.

##Bedingungen
Die Lösung wird zunächst in der pragmatischsten Form umgesetzt, als Console Application. Später, wenn klar ist welche
Teile in der Appliaktion zusammenspielen, können diese separiert werden.



