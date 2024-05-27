package agentie.client.Controllers;

import agentie.model.Angajat;
import agentie.model.Excursie;
import agentie.services.AgentieException;
import agentie.services.IObserver;
import agentie.services.IService;
import javafx.application.Platform;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Node;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.layout.AnchorPane;
import javafx.scene.paint.Color;
import javafx.stage.Modality;
import javafx.stage.Stage;

import java.io.IOException;
import java.time.LocalDateTime;
import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

public class AngajatController implements IObserver {
    IService server;
    Angajat angajat;
    Stage angajatStage;
    Stage logInStage;

    ObservableList<Excursie> modelToateExcursii = FXCollections.observableArrayList();
    ObservableList<Excursie> modelExcursiiCautate = FXCollections.observableArrayList();
    @FXML
    public TextField numeObiectivTextField;
    @FXML
    public TextField deLaOraTextField;
    @FXML
    public TextField panaLaOraTextField;

    @FXML
    public TableView<Excursie> toateExcursiileTableView;
    @FXML
    public TableView<Excursie> excursiiCautateTableView;
    @FXML
    public TableColumn<Excursie, String> tableColumnObiectiv1;
    @FXML
    public TableColumn<Excursie, String> tableColumnObiectiv2;
    @FXML
    public TableColumn<Excursie, String> tableColumnFirma1;
    @FXML
    public TableColumn<Excursie, String> tableColumnFirma2;
    @FXML
    public TableColumn<Excursie, LocalDateTime> tableColumnOraPlecare1;
    @FXML
    public TableColumn<Excursie, LocalDateTime> tableColumnOraPlecare2;
    @FXML
    public TableColumn<Excursie, Integer> tableColumnNumarLocuriDisponibile1;
    @FXML
    public TableColumn<Excursie, Integer> tableColumnNumarLocuriDisponibile2;
    @FXML
    public TableColumn<Excursie, Double> tableColumnPret1;
    @FXML
    public TableColumn<Excursie, Double> tableColumnPret2;

    public void setServer(IService server, Angajat angajat, Stage angajatStage) throws AgentieException {
        this.server = server;
        this.angajat = angajat;
        this.angajatStage = angajatStage;
        initModelToateExcursii();
    }

    public void setStage(Stage stage) {
        this.logInStage = stage;
    }

    @FXML
    public void initialize() {
        tableColumnObiectiv1.setCellValueFactory(new PropertyValueFactory<>("obiectivTuristic"));
        tableColumnObiectiv2.setCellValueFactory(new PropertyValueFactory<>("obiectivTuristic"));
        tableColumnFirma1.setCellValueFactory(new PropertyValueFactory<>("numeFirmaTransport"));
        tableColumnFirma2.setCellValueFactory(new PropertyValueFactory<>("numeFirmaTransport"));
        tableColumnOraPlecare1.setCellValueFactory(new PropertyValueFactory<>("oraPlecare"));
        tableColumnOraPlecare2.setCellValueFactory(new PropertyValueFactory<>("oraPlecare"));
        tableColumnNumarLocuriDisponibile1.setCellValueFactory(new PropertyValueFactory<>("nrLocuriDisponibile"));
        tableColumnNumarLocuriDisponibile1.setCellFactory(column -> new TableCell<>() {
            @Override
            protected void updateItem(Integer item, boolean empty) {
                super.updateItem(item, empty);
                if (empty || item == null) {
                    setText(null);
                    setStyle("");
                } else {
                    setText(item.toString());
                    // Check if number of available seats is zero
                    if (item == 0) {
                        setTextFill(Color.RED);
                        setRowTextFill(Color.RED);
                    } else {
                        setTextFill(Color.BLACK);
                        setRowTextFill(Color.BLACK);
                    }
                }
            }
            private void setRowTextFill(Color color) {
                getTableRow().getChildrenUnmodifiable().forEach(cell -> ((TableCell<?, ?>) cell).setTextFill(color));
            }
        });
        tableColumnNumarLocuriDisponibile2.setCellValueFactory(new PropertyValueFactory<>("nrLocuriDisponibile"));
        tableColumnPret1.setCellValueFactory(new PropertyValueFactory<>("pret"));
        tableColumnPret2.setCellValueFactory(new PropertyValueFactory<>("pret"));
        toateExcursiileTableView.setItems(modelToateExcursii);
        excursiiCautateTableView.setItems(modelExcursiiCautate);

    }

    public void initModelToateExcursii() throws AgentieException {
        Iterable<Excursie> excursii = server.findAllExcursii();
        List<Excursie> excursiiList = StreamSupport.stream(excursii.spliterator(), false)
                .collect(Collectors.toList());
        modelToateExcursii.setAll(excursiiList);
    }

    public void handleCautaButton() throws AgentieException {
        String numeObiectiv = numeObiectivTextField.getText().trim();
        int deLaOra = Integer.parseInt(deLaOraTextField.getText().trim());
        int panaLaOra = Integer.parseInt(panaLaOraTextField.getText().trim());

        if (deLaOra > panaLaOra) {
            AlertMessage.showErrorMessage(null, "Ora de inceput trebuie sa fie mai mica decat ora de sfarsit!");
            return;
        } else if (deLaOra < 0 || deLaOra > 24 || panaLaOra > 24) {
            AlertMessage.showErrorMessage(null, "Ora trebuie sa fie intre 0 si 24!");
            return;
        }

        excursiiCautateTableView.getItems().clear();
        List<Excursie> excursiiCautate = (List<Excursie>) server.cautaExcursii(numeObiectiv, deLaOra, panaLaOra);
        List<Excursie> excursiiValabile = excursiiCautate.stream()
                .filter(excursie -> excursie.getNrLocuriDisponibile() > 0)
                .toList();
        modelExcursiiCautate.setAll(excursiiValabile);
    }

    public void handleRezervaLocuriButton() throws IOException {
        Excursie excursieSelectata = excursiiCautateTableView.getSelectionModel().getSelectedItem();
        if (excursieSelectata == null) {
            AlertMessage.showErrorMessage(null, "Trebuie selectata o excursie!");
            return;
        }
        FXMLLoader loader = new FXMLLoader();
        loader.setLocation(getClass().getResource("/rezervareView.fxml"));
        AnchorPane root = loader.load();
        Stage rezervareStage = new Stage();
        rezervareStage.setTitle("Rezerva locuri");
        rezervareStage.initModality(Modality.WINDOW_MODAL);

        Scene scene = new Scene(root);
        rezervareStage.setScene(scene);

        RezervareController rezervareController = loader.getController();
        rezervareController.setServer(server, excursieSelectata, rezervareStage, angajat);
        modelExcursiiCautate.clear();
        rezervareStage.show();
    }

    public void handleLogOutButton(javafx.event.ActionEvent actionEvent) {
        Alert alert = new Alert(Alert.AlertType.CONFIRMATION);
        alert.setTitle("Logout Confirmation");
        alert.setHeaderText("Are you sure you want to log out?");
        alert.setContentText("Click OK to confirm, or Cancel to stay logged in.");

        alert.showAndWait().ifPresent(response -> {
            if (response == javafx.scene.control.ButtonType.OK) {
                try {
                    server.logout(angajat);
                    ((Node)(actionEvent.getSource())).getScene().getWindow().hide();
                    logInStage.show();
                } catch (AgentieException e) {
                    System.out.println("Logout error " + e);
                }

            }
        });
    }

    @Override
    public void updateNumarLocuriExcursie(Excursie excursie) {
        Platform.runLater(() -> {
            // Find the index of the Excursie object in the list
            int index = -1;
            for (int i = 0; i < modelToateExcursii.size(); i++) {
                if (modelToateExcursii.get(i).getId().equals(excursie.getId())) {
                    index = i;
                    break;
                }
            }
            // If the Excursie object is found, update it in the list
            if (index != -1) {
                modelToateExcursii.set(index, excursie);
            }
        });
    }
}
