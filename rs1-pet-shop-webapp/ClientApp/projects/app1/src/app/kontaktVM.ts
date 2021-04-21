export class Kontakti {
  id: number;
  ime: string;
  email: string;
  text: string;
  odgovoreno: boolean;
  odgovor: string;
  korisnikid: number;
}

export interface KontaktiPrikaz {
  kontakti: Kontakti[];
}

export class Odgovor {
  id: number;
  ime: string;
  email: string;
  text: string;
  odgovoreno: boolean;
  adminId: number;
  admin?: any;
  odgovor: string;
}
