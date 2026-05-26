# Programmieraufgabe: Rezeptverwaltung als .NET-Bibliothek

## Ziel
Entwickeln Sie eine .NET-Klassenbibliothek in C#, die eine Rezept-, Kategorien- und Nutzerverwaltung bereitstellt. 

Die Lösung soll sich wie produktionsreifer Code verhalten und demonstrieren, wie Sie reale Anforderungen analysieren und umsetzen.

## Schwerpunkte
Legen Sie besonderen Wert auf:

- klare Struktur und eindeutige Verantwortlichkeiten  
- nachvollziehbare Architektur- und Designentscheidungen  
- Codequalität (verständlich, wartbar, erweiterbar, testbar, gut analysierbar)  
- Anwendung gängiger Softwareentwicklungsprinzipien  

## Vorgaben
- Sprache/Framework: C# im .NET-Ökosystem  
- Architektur, Tooling und externe Bibliotheken: frei wählbar, passend zur Aufgabe und zu Ihren Kenntnissen  

## Erwartete Ergebnisse
- Link zu einem öffentlichen Git-Repository 
- Eine kurze README mit Installations-/Startanleitung und Architekturüberblick
- Eine .NET Klassenbibliothek
- Ein Beispiel-Projekt zur Demonstration der Bibliothek

### Die .NET Klassenbibliothek

- kapselt die Logik zur Umsetzung der fachlichen Anforderungen
- stellt die geforderten Funktionalitäten über eine einfache API zur Verfügung
- kann in beliebigen Anwendungen (Desktop-App, Website, WebAPI) genutzt werden
- ermöglicht es die Daten dauerhaft zu speichern (Technologie und Struktur frei wählbar)

### Das Beispiel-Projekt zur Demonstration
Erstellen Sie ein kleines Zusatzprojekt, das die Bibliothek in typischen Anwendungsfällen nutzt. Keine komplexe UI erforderlich.

Wichtig: Das Demoprojekt darf selbst keine fachliche Logik enthalten. Es demonistriert lediglich die entwickelte Bibliothek über die dafür vorgesehene API (d.h. kein direkter Zugriff auf Persistenz oder interna der Bibliothek) 


## Fachliche Anforderungen

### Nutzerverwaltung
- Nur registrierte Benutzer dürfen Rezepte verwalten  
- Eine Benutzerverwaltung ist erforderlich  

### Rezeptverwaltung
- Benutzer können Rezepte anlegen, bearbeiten und löschen  
- Einschränkungen:
  - Rezeptnamen sind global eindeutig  
  - Ein Rezept enthält mindestens einen Zubereitungsschritt  
  - Ein Rezept enthält mindestens eine Zutat  
  - Ein Rezept ist mindestens einer Kategorie zugeordnet  
- Zutaten:
  - stammen aus einer globalen, user-unabhängigen Zutatenliste  
  - neue Zutaten können hinzugefügt werden  

### Kategorienverwaltung
- Kategorien können angelegt, bearbeitet und gelöscht werden  
- Kategorienamen sind eindeutig  

### Abfragen & Favoriten
- Rezepte eines bestimmten Nutzers anzeigen  
- Rezepte nach Kategorie anzeigen  
- Rezepte nach Zutat anzeigen  
- Optional: Rezepte anderer Benutzer als Favorit markieren/demarkieren  
- Optional: Favoriten eines Users anzeigen