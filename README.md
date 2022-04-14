Kreirana je WPF aplikacija za monitoring izmerenih vrednosti elemenata sistema/mreže (NetworkService). Pored aplikacije za monitoring, koristiće se i simulatorska 
aplikacija (MeteringSimulator, već implementirana) koja će u nasumično izabranim vremenskim trenucima, slati nasumične brojne vrednosti, koje će aplikacija za 
monitoring obraditi i iskoristiti. 

Svaka nova brojevna vrednost dolazi od simulatorske aplikacije zajedno sa indeksom entiteta u listi unutar NetworkService aplikacije za kojeg je to nova vrednost, 
što se memoriše u Log fajlu (.txt) na disku sistema, uz vremenski trenutak kada se promena desila. 

Samu aplikaciju čine tri View-a:
* Network Entities: Omogućeno je dodavanje i brisanje entiteta za monitoring i osnovne podaci o njima su čuvani u vidu tabele (novodobijena 
brojevna vrednost se prikazuje u jednoj od kolona tabele). Korisnik ima priliku da pretražuje ili filtrira prikaz u tabeli. Nakon dodavanja ili brisanja objekata, 
simulatorska aplikacija mora da se restartuje (njeno funkcionisanje se NE SME menjati) i ona će tada sama da prikupi podatke koliko ukupno postoji objekata u 
(NetworkService) aplikaciji, za koje treba da daje podatke o stanju. Ispod tabele postoji forma za dodavanje novog entiteta u skladu sa njegovim atributima.

* Network Display: Sadrži prostor gde će se nalaziti vizuelni prikazi entiteta i simulirati njihovo mesto u sistemu/mreži. Njihov raspored nije bitan i njega 
određuje korisnik tako što pomera te prikaze (slike) drag&drop tehnikom. Dodatno je omogućena vizuelna izmena ukoliko novoizmerena vrednost bude ispod ili iznad 
zadate granice. Vizuelna izmena može biti u vidu notifikacije, promene boje, ili promene statusne slike. Pored svega je omogućeno da se objekti mogu spajati linijama.

* Measurement Graph: Na osnovu podataka zapisanih u Log fajlu, prikazana je istorija stanja pomoću grafikona. Grafikoni će se konstantno menjati, jer se konstantno 
dobijaju nove informacije, ali idu unazad pet poslednjih merenja. 
