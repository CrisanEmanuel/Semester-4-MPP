package agentie.client;

import agentie.client.Controllers.LogInController;
import agentie.networking.protocolBuffers.AgentieServicesProtoProxy;
import agentie.services.IService;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;

import java.io.IOException;
import java.util.Properties;

public class StartProtoClientFX extends Application {

    private static final int defaultAgentiePort = 55555;
    private static final String defaultServer = "localhost";

    public void start(Stage primaryStage) throws Exception {
        System.out.println("In start");
        Properties clientProps = new Properties();
        try {
            clientProps.load(StartRpcClientFX.class.getResourceAsStream("/agentieclientfx.properties"));
            System.out.println("Client properties set. ");
            clientProps.list(System.out);
        } catch (IOException e) {
            System.err.println("Cannot find agentieclientfx.properties " + e);
            return;
        } catch (Exception e) {
            System.err.println("Cannot read agentieclientfx.properties " + e);
        }
        String serverIP = clientProps.getProperty("agentie.server.host", defaultServer);
        int serverPort = defaultAgentiePort;

        try {
            serverPort = Integer.parseInt(clientProps.getProperty("agentie.server.port"));
        } catch (NumberFormatException ex) {
            System.err.println("Wrong port number " + ex.getMessage());
            System.out.println("Using default port: " + defaultAgentiePort);
        }
        System.out.println("Using server IP " + serverIP);
        System.out.println("Using server port " + serverPort);

        IService server = new AgentieServicesProtoProxy(serverIP, serverPort);

        FXMLLoader loader = new FXMLLoader(getClass().getClassLoader().getResource("logInView.fxml"));
        Parent root = loader.load();
        primaryStage.setTitle("Login!");
        primaryStage.setScene(new Scene(root));
        LogInController logInController = loader.getController();
        logInController.setServer(server);
        logInController.setStage(primaryStage);
        primaryStage.show();
    }
}
