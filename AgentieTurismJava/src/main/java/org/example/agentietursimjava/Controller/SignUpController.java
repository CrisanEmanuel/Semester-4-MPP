package org.example.agentietursimjava.Controller;

import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.TextField;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Modality;
import javafx.stage.Stage;
import org.example.agentietursimjava.Domain.Angajat;
import org.example.agentietursimjava.Service.Service;

import java.io.IOException;
import java.util.Optional;

import static org.example.agentietursimjava.Utils.Password.checkPasswordFormat;
import static org.example.agentietursimjava.Utils.Password.hashPassword;

public class SignUpController {

    Service service;

    @FXML
    public TextField numeTextField;
    @FXML
    public TextField prenumeTextField;
    @FXML
    public TextField agentieTextField;
    @FXML
    public TextField usernameTextField;
    @FXML
    public TextField parolaTextField;
    @FXML
    public TextField confirmaParolaTextField;

    public void setService(Service service) {
        this.service = service;
    }

    public void handleSignUpButton() {
        String nume = numeTextField.getText().trim();
        String prenume = prenumeTextField.getText().trim();
        String agentie = agentieTextField.getText().trim();
        String username = usernameTextField.getText().trim();
        String parola = parolaTextField.getText().trim();
        String confirmaParola = confirmaParolaTextField.getText().trim();

        if (nume.isEmpty() || prenume.isEmpty() || agentie.isEmpty() || username.isEmpty() || parola.isEmpty() || confirmaParola.isEmpty()) {
            AlertMessage.showErrorMessage(null, "Toate campurile sunt obligatorii!");
            return;
        }

        if (!parola.equals(confirmaParola)) {
            AlertMessage.showErrorMessage(null, "Parolele nu coincid!");
            return;
        }

        if (service.findAngajatByUsername(username).isPresent()) {
            AlertMessage.showErrorMessage(null, "Username-ul exista deja!");
            return;
        }

        if (!checkPasswordFormat(parola)) {
            AlertMessage.showErrorMessage(null, """
                    Password must be at least 8 characters long and contain at least one of the following characters:
                    @#$%^&+=!
                    capital letter
                    number""");
            return;
        }

        String hashedPassword = hashPassword(parola);
        Angajat angajat = new Angajat(nume, prenume, username, hashedPassword, agentie);
        try {
            Optional<Angajat> angajatAdaugat = service.addAngajat(angajat);
            if (angajatAdaugat.isEmpty()) {
                AlertMessage.showMessage(null, Alert.AlertType.CONFIRMATION, "Cont creat", "Contul a fost creat cu succes!");
            } else {
                AlertMessage.showErrorMessage(null, "Angajat deja exist!");
            }
            numeTextField.clear();
            prenumeTextField.clear();
            agentieTextField.clear();
            usernameTextField.clear();
            parolaTextField.clear();
            confirmaParolaTextField.clear();
        } catch (RuntimeException e) {
            AlertMessage.showErrorMessage(null, e.getMessage());
        }
    }

    public void handleLogInHyperlink() throws IOException {
        FXMLLoader loader = new FXMLLoader();
        loader.setLocation(getClass().getResource("/org/example/agentietursimjava/logInView.fxml"));

        AnchorPane root = loader.load();

        Stage logInStage = new Stage();
        logInStage.setTitle("Log In");
        logInStage.initModality(Modality.WINDOW_MODAL);

        Scene scene = new Scene(root);
        logInStage.setScene(scene);

        LogInController logInController = loader.getController();
        logInController.setService(service);
        logInStage.show();
    }
}
