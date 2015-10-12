#CQRS Demo

##Inhalt
Diese Demo Applikation besteht aus zwei branches. Der `master` branch enth�lt eine leere Solution, 
mit der das vorliegende Beispiel erstellt werden kann.
Der zweite branch namens `complete-solution` steht erst nach der ADC 15 zur Verf�gung, damit keiner schummelt ;-)

Die L�sung wird mit Hilfe des **fluent-CQRS** Frameworks entwickelt.

Das Verwalten der externen Libraries erfolgt mit Hilfe des Paketierungs Managers [Paket](http://fsprojects.github.io/Paket/).
Um sich die Arbeit zu erleichtern, sollte man sich die Paket Extension f�r Visual Studio installieren. Diese erkennt
die `paket.dependencies` Datei, mit der man weitere gew�nschte Packages einbinden kann.

##Setup
Nach dem �ffenen der Solution und dem Installieren der Paket Extensions, bitte mit einem Rechtsklick auf die 
`paket.dependencies` Datei die Installation der Pakete starten.

~Fertig

##Aufgabe
Es soll eine Applikation gebaut werden, mit der man einen Mietvertrag vorbereiten kann und bei Einzug des
Mieters wird diese Vorbereitung abgeschlossen.

###Mietvertrag vorbereiten
Wenn ein Mieter in Verhandlung mit einem Vermiter steht, kann die Wohnung w�hrend dieses Zeitraumes reserviert werden.
Unter Angabe der Adressdaten, einer Einheitennummer und den Mieterdaten sowie dem geplanten Einzugstermin kann
eine Reservierung und Vorbereitung durchgef�hrt werden.

###Einzug
Wenn eine Reservierung vorligt und die Vorbereitung durchgef�hrt wurde, kann der Einzug im System gespeichert werden. 
Dazu muss zus�tzlich der tats�chliche Einzugstermin angegeben werden.

Liegt noch keine Reservierung bzw. Vorbereitung vor, wird ein Business Fehler (`Fault`) geworfen und ausgegeben.

##Bedingungen
Die L�sung wird zun�chst in der pragmatischsten Form umgesetzt, als Console Application. Sp�ter, wenn klar ist welche
Teile in der Appliaktion zusammenspielen, k�nnen diese separiert werden.



