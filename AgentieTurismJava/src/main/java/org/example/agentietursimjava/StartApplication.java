package org.example.agentietursimjava;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;
import org.example.agentietursimjava.Controller.LogInController;
import org.example.agentietursimjava.Repository.DBRepos.AngajatDBRepo;
import org.example.agentietursimjava.Repository.DBRepos.ExcursieDBRepo;
import org.example.agentietursimjava.Repository.DBRepos.RezervareDBRepo;
import org.example.agentietursimjava.Service.AngajatService;
import org.example.agentietursimjava.Service.ExcursieService;
import org.example.agentietursimjava.Service.RezervareService;
import org.example.agentietursimjava.Service.Service;

import java.io.FileReader;
import java.io.IOException;
import java.util.Properties;

public class StartApplication extends Application {

    private Service service;

    @Override
    public void start(Stage primaryStage) throws Exception {

        Properties props = new Properties();
        try {
            props.load(new FileReader("application.properties"));
        } catch (IOException e) {
            System.out.println("Cannot find application.properties " + e);
        }
        AngajatDBRepo angajatDBRepo = new AngajatDBRepo(props);
        ExcursieDBRepo excursieDBRepo = new ExcursieDBRepo(props);
        RezervareDBRepo rezervareDBRepo = new RezervareDBRepo(props);
        AngajatService angajatService = new AngajatService(angajatDBRepo);
        ExcursieService excursieService = new ExcursieService(excursieDBRepo);
        RezervareService rezervareService = new RezervareService(rezervareDBRepo);
        service = new Service(angajatService, excursieService, rezervareService);
        initView(primaryStage);
        primaryStage.show();
    }

    public static void main(String[] args) {
        launch();
    }

    private void initView(Stage primaryStage) throws IOException {

        FXMLLoader userLoader = new FXMLLoader();
        userLoader.setLocation(getClass().getResource("logInView.fxml"));
        AnchorPane userLayout = userLoader.load();
        primaryStage.setTitle("Log in");
        primaryStage.setScene(new Scene(userLayout));

        LogInController logInController = userLoader.getController();
        logInController.setService(service);
    }
}
