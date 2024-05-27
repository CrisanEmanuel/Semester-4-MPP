package agentie.networking.dto;

import agentie.model.Angajat;

public class dtoUtils {
    public static Angajat getFromDTO(dtoAngajat angajat){
        String username = angajat.getUsername();
        String password = angajat.getPassword();
        return new Angajat(username, password);

    }
    public static dtoAngajat getDTO(Angajat angajat){
        String username = angajat.getUsername();
        String password = angajat.getPassword();
        return new dtoAngajat(username, password);
    }
}
