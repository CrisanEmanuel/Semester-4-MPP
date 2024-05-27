import agentie.model.Excursie;
import agentie.model.ExcursieDTO;
import org.springframework.http.HttpEntity;
import org.springframework.http.HttpMethod;
import org.springframework.http.ResponseEntity;
import org.springframework.web.client.RestTemplate;
import startRestServices.StartRestServices;

import java.time.LocalDateTime;
import java.util.Arrays;
import java.util.List;

import java.util.UUID;

class RestClient {
    private final String baseUrl;
    private final RestTemplate restTemplate;

    public RestClient(String baseUrl) {
        this.baseUrl = baseUrl;
        this.restTemplate = new RestTemplate();
    }

    public Excursie findExcursieById(UUID id) {
        String findOneUrl = baseUrl + "/agentie-turism/excursii/" + id;
        ResponseEntity<Excursie> response = restTemplate.getForEntity(findOneUrl, Excursie.class);
        if (response.getStatusCode().is2xxSuccessful()) {
            return response.getBody();
        } else {
            System.out.println("Excursie with ID " + id + " not found");
            return null;
        }
    }

    public List<Excursie> findAllExcursii() {
        String findAllUrl = baseUrl + "/agentie-turism/excursii";
        ResponseEntity<Excursie[]> responseEntity = restTemplate.getForEntity(findAllUrl, Excursie[].class);
        Excursie[] excursiiArray = responseEntity.getBody();
        if (excursiiArray != null) {
            return Arrays.asList(excursiiArray);
        } else {
            return null;
        }
    }

    public UUID createExcursie(ExcursieDTO excursie) {
        String createUrl = baseUrl + "/agentie-turism/excursii";
        ResponseEntity<UUID> response = restTemplate.postForEntity(createUrl, excursie, UUID.class);
        if (response.getStatusCode().is2xxSuccessful()) {
            System.out.println("Excursie created successfully");
            return response.getBody();
        } else {
            System.out.println("Failed to create excursie. Status code: " + response.getStatusCode());
            return null;
        }
    }

    public void deleteExcursie(Excursie excursie) {
        String deleteUrl = baseUrl + "/agentie-turism/excursii/" + excursie.getId();
        ResponseEntity<Void> response = restTemplate.exchange(deleteUrl, HttpMethod.DELETE, null, Void.class);
        if (response.getStatusCode().is2xxSuccessful()) {
            System.out.println("Excursie with ID " + excursie.getId() + " deleted successfully");
        } else {
            System.err.println("Failed to delete excursie. Status code: " + response.getStatusCode());
        }
    }

    public void updateExcursie(Excursie excursie) {
        String updateUrl = baseUrl + "/agentie-turism/excursii/" + excursie.getId();
        HttpEntity<Excursie> requestEntity = new HttpEntity<>(excursie);
        ResponseEntity<Void> response = restTemplate.exchange(updateUrl, HttpMethod.PUT, requestEntity, Void.class);

        if (response.getStatusCode().is2xxSuccessful()) {
            System.out.println("Excursie with ID " + excursie.getId() + " updated successfully");
        } else {
            System.err.println("Failed to update excursie. Status code: " + response.getStatusCode());
        }
    }
}

class TestEndpoints {
    public static void main(String[] args) {
       // StartRestServices.main(args);
        RestClient restClient = new RestClient("http://localhost:8080");

        ExcursieDTO excursiedto = new ExcursieDTO("testing", "testing", LocalDateTime.now().toString(), 50, 10);
        UUID id = restClient.createExcursie(excursiedto);
        System.out.println(restClient.findExcursieById(id));
        restClient.findAllExcursii().forEach(System.out::println);

        Excursie newEx = new Excursie("new", "new", LocalDateTime.now(), 50, 10);
        newEx.setId(id);
        restClient.updateExcursie(newEx);
        restClient.deleteExcursie(newEx);
    }
}