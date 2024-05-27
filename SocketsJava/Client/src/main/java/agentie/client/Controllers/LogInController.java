package agentie.client.Controllers;

import agentie.model.Angajat;
import agentie.services.AgentieException;
import agentie.services.IService;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import javafx.stage.Modality;
import javafx.stage.Stage;

import java.io.IOException;

public class LogInController {
    IService server;
    Stage primaryStage;
    @FXML
    public TextField usernameTextField;
    @FXML
    public PasswordField passwordTextField;

    public void setServer(IService server) {
        this.server = server;
    }

    public void setStage(Stage stage) {
        this.primaryStage = stage;
    }

    public void handleLogInButton() throws IOException, AgentieException {
        if (usernameTextField.getText().isEmpty() || passwordTextField.getText().isEmpty()) {
            AlertMessage.showErrorMessage(null, "Invalid username or password");
            return;
        }

        FXMLLoader loader = new FXMLLoader(getClass().getClassLoader().getResource("angajatView.fxml"));
        Parent root = loader.load();

        Stage angajatStage = new Stage();
        angajatStage.setTitle("Welcome back " + usernameTextField.getText().trim() + "!");
        angajatStage.initModality(Modality.WINDOW_MODAL);
        angajatStage.setScene(new Scene(root));

        AngajatController angajatController = loader.getController();
        // open a thread for user
        Angajat angajat = new Angajat(usernameTextField.getText(), passwordTextField.getText());
        try {
            server.login(angajat, angajatController);
        } catch (AgentieException e) {
            AlertMessage.showErrorMessage(null, e.getMessage());
            return;
        }
        angajatController.setServer(server, angajat, angajatStage);
        angajatController.setStage(primaryStage);

        primaryStage.hide();
        angajatStage.show();

        usernameTextField.clear();
        passwordTextField.clear();
    }

    public void handleSignUpHyperlink() {
        // nothing
    }
}
