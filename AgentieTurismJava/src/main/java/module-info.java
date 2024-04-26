module org.example.agentietursimjava {
    requires javafx.controls;
    requires javafx.fxml;
    requires org.apache.logging.log4j;
    requires java.sql;


    opens org.example.agentietursimjava to javafx.fxml;

    exports org.example.agentietursimjava;
    exports org.example.agentietursimjava.Service;
    exports org.example.agentietursimjava.Controller;
    exports org.example.agentietursimjava.Domain;
    exports org.example.agentietursimjava.Repository.DBRepos;

}