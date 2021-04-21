import { Component } from '@angular/core';
import { Kontakti, KontaktiPrikaz, Odgovor } from './kontaktVM';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MyConfig } from './MyConfig';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app1';

  kon: KontaktiPrikaz = null;
  odg: Odgovor = null;
  provjera: boolean = false;
  provjeraOdgovor: boolean = false;
  provjeraFaq: boolean = false;

  constructor(private http: HttpClient) {

  }

  preuzmiPodatke() {
    this.http.get<KontaktiPrikaz>(MyConfig.adresaServer + "/Kontakt/Index").subscribe(result => {
      this.kon = result;
    });
  }

  tableToggle() {
    this.http.get<KontaktiPrikaz>(MyConfig.adresaServer + "/Kontakt/Index").subscribe(result => {
      this.kon = result;
    });

    this.provjera = !this.provjera;
    this.provjeraOdgovor = false;
    this.provjeraFaq = false;
  }

  odgovorToggle(k: Kontakti) {
    this.http.get<Odgovor>(MyConfig.adresaServer + "/Kontakt/vratiOdgovor?id=" + k.id).subscribe(result => {
      this.odg = result;
    });

    this.provjera = false;
    this.provjeraFaq = false;
    this.provjeraOdgovor = !this.provjeraOdgovor;
  }

  spremiPodatke(val) {
    var httpOpcije = {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }

    this.http.post(MyConfig.adresaServer + "/Kontakt/Snimi", val, httpOpcije).subscribe(result => {
    });

    alert("Upit uspješno poslan");
  }

  faqToggle() {
    this.provjera = false;
    this.provjeraOdgovor = false;
    this.provjeraFaq = true;
  }
}
