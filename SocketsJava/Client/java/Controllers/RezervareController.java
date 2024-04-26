package agentie.client.Controllers;

import agentie.model.Angajat;
import agentie.services.AgentieException;
import agentie.services.IService;
import javafx.fxml.FXML;
import javafx.scene.control.Alert;
import javafx.scene.control.TextField;
import javafx.scene.text.Text;
import javafx.stage.Stage;
import agentie.model.Rezervare;
import agentie.model.Excursie;

public class RezervareController {
    IService server;
    Excursie excursie;
    Stage stage;
    Angajat angajat;
    @FXML
    public Text textObiectivTuristic;
    @FXML
    public Text textNumarLocuri;
    @FXML
    public TextField numeTextField;
    @FXML
    public TextField numarTelefonTextField;
    @FXML
    public TextField numarLocuriTextField;


    public void setServer(IService server, Excursie excursie, Stage stage, Angajat angajat) {
        this.server = server;
        this.excursie = excursie;
        this.stage = stage;
        this.angajat = angajat;
        textObiectivTuristic.setText(excursie.getObiectivTuristic());
        textNumarLocuri.setText(String.valueOf(excursie.getNrLocuriDisponibile()));
    }
    public void handleRezervaButton() {
        String nume = numeTextField.getText().trim();
        String numarTelefon = numarTelefonTextField.getText().trim();
        String nrBileteText = numarLocuriTextField.getText().trim();

        if (nume.isEmpty() || numarTelefon.isEmpty() || numarLocuriTextField.getText().isEmpty()) {
            AlertMessage.showErrorMessage(null, "Toate campurile sunt obligatorii!");
            return;
        }

        int nrBilete = Integer.parseInt(nrBileteText);

        if (nrBilete > excursie.getNrLocuriDisponibile()) {
            AlertMessage.showErrorMessage(null, "Nu sunt suficiente locuri disponibile!");
            return;
        }
        if (nrBilete <= 0) {
            AlertMessage.showErrorMessage(null, "Numarul de locuri trebuie sa fie pozitiv!");
            return;
        }

        try {
            server.addRezervare(new Rezervare(nume, numarTelefon, nrBilete, excursie));
            Excursie excursieModificata = new Excursie(excursie.getObiectivTuristic(), excursie.getNumeFirmaTransport(),
                    excursie.getOraPlecare(), excursie.getNrLocuriDisponibile() - nrBilete, excursie.getPret());
            excursieModificata.setId(excursie.getId());
            server.updateExcursie(excursieModificata);
            AlertMessage.showMessage(null, Alert.AlertType.INFORMATION, "Rezervare efectuata",
                    "Ati rezervat " + nrBilete + " locuri pentru excursia catre " + excursie.getObiectivTuristic() + "!"
            + "\nDate contact client\nNume: " + nume + "\nNumar telefon: " + numarTelefon);
            stage.close();
        } catch (RuntimeException e) {
            AlertMessage.showErrorMessage(null, e.getMessage());
        } catch (AgentieException e) {
            throw new RuntimeException(e);
        }
    }
}
