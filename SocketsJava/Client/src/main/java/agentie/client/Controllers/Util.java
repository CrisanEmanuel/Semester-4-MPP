package agentie.client.Controllers;

import javafx.scene.control.Alert;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

public class Util {

    private static final Logger logger = LogManager.getLogger(Util.class.getName());

    public static void showWarning(String header, String content){
        Alert alert = new Alert(Alert.AlertType.INFORMATION);
        alert.setTitle("MPP agentie");
        alert.setHeaderText(header);
        alert.setContentText(content);
        alert.showAndWait();
    }
}