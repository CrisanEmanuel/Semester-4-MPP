package startRestServices;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.ComponentScan;

import java.io.FileReader;
import java.io.IOException;
import java.util.Properties;

@ComponentScan({"restController", "agentie.persistence"})
@SpringBootApplication
public class StartRestServices {

    public static void main(String[] args) {
        SpringApplication.run(StartRestServices.class, args);
    }

    @Bean(name="props")
    public Properties getBdProperties(){
        Properties props = new Properties();
        try {
            props.load(new FileReader("REST/src/main/resources/bd.config"));
        } catch (IOException e) {
            System.err.println("Configuration file bd.cong not found " + e);

        }
        System.out.println(props.getProperty("jdbc.url"));
        return props;
    }
}
