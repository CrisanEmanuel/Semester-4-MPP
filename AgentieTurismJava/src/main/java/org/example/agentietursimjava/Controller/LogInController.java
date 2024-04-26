package org.example.agentietursimjava.Controller;

import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Modality;
import javafx.stage.Stage;
import org.example.agentietursimjava.Domain.Angajat;
import org.example.agentietursimjava.Service.Service;

import java.io.IOException;
import java.util.Optional;

import static org.example.agentietursimjava.Utils.Password.verifyPassword;

public class LogInController {
    Service service;
    @FXML
    public TextField usernameTextField;
    @FXML
    public PasswordField passwordTextField;
    public void setService(Service service) {
        this.service = service;
    }

    public void handleLogInButton() throws IOException {
        FXMLLoader loader = new FXMLLoader();
        loader.setLocation(getClass().getResource("/org/example/agentietursimjava/angajatView.fxml"));

        AnchorPane root = loader.load();

        if (usernameTextField.getText().isEmpty() || passwordTextField.getText().isEmpty()) {
            AlertMessage.showErrorMessage(null, "Invalid username or password");
            return;
        }

        // check if the user is in the database
        Optional<Angajat> angajat = service.findAngajatByUsername(usernameTextField.getText().trim());
        if (angajat.isEmpty()) {
            AlertMessage.showErrorMessage(null, "Angajat " + usernameTextField.getText().trim() + " doesn't exist!");
            return;
        } else {
            if (!verifyPassword(passwordTextField.getText().trim(), angajat.get().getPassword())) {
                AlertMessage.showErrorMessage(null, "Invalid username or password!");
                return;
            }
        }

        Stage angajatStage = new Stage();
        angajatStage.setTitle("Welcome back " + usernameTextField.getText().trim() + "!");
        angajatStage.initModality(Modality.WINDOW_MODAL);

        Scene scene = new Scene(root);
        angajatStage.setScene(scene);

        ExcursieController excursieController = loader.getController();
        excursieController.setService(service, usernameTextField.getText().trim(), angajatStage);

        angajatStage.show();

        usernameTextField.clear();
        passwordTextField.clear();
    }

    public void handleSignUpHyperlink() throws IOException {
        FXMLLoader loader = new FXMLLoader();
        loader.setLocation(getClass().getResource("/org/example/agentietursimjava/signUpView.fxml"));

        AnchorPane root = loader.load();

        Stage signUpStage = new Stage();
        signUpStage.setTitle("Sign Up");
        signUpStage.initModality(Modality.WINDOW_MODAL);

        Scene scene = new Scene(root);
        signUpStage.setScene(scene);

        SignUpController signUpController = loader.getController();
        signUpController.setService(service);
        signUpStage.show();
    }
}
