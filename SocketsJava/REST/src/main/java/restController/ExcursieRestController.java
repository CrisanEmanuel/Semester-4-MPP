package restController;

import agentie.model.Excursie;
import agentie.model.ExcursieDTO;
import agentie.persistence.Interfaces.IRepoExcursie;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

@RestController
@RequestMapping("agentie-turism/excursii")
@CrossOrigin(origins = "*", allowedHeaders = "*")
public class ExcursieRestController {

    private final IRepoExcursie repoExcursie;

    public ExcursieRestController(IRepoExcursie repoExcursie) {
        this.repoExcursie = repoExcursie;
    }

    @GetMapping("test")
    public String test(@RequestParam(value="name", defaultValue="Hello") String name) {
        return name.toUpperCase();
    }

    @PostMapping
    public ResponseEntity<?> create(@RequestBody ExcursieDTO excursieDTO) {
        System.out.println("Creating excursie");
        String oraString = excursieDTO.getOraPlecare();
        var oraToJavaLocalDateTime = java.time.LocalDateTime.parse(oraString);
        var excursie = new Excursie(excursieDTO.getObiectivTuristic(), excursieDTO.getNumeFirmaTransport(), oraToJavaLocalDateTime, excursieDTO.getNrLocuriDisponibile(), excursieDTO.getPret());
        excursie.setId(UUID.randomUUID());
        var result = repoExcursie.save(excursie); // empty if successful, the entity if not
        if (result.isEmpty())
            return new ResponseEntity<>(excursie.getId(), HttpStatus.CREATED);
        else
            return new ResponseEntity<>(excursie.getId(), HttpStatus.CONFLICT);
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<?> delete(@PathVariable UUID id) {
        System.out.println("Deleting excursie");
        var result = repoExcursie.delete(id); // entity if successful, empty if not
        if (result.isPresent())
            return new ResponseEntity<>(id, HttpStatus.OK);
        else
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
    }

    @PutMapping("/{id}")
    public ResponseEntity<?> update(@PathVariable UUID id, @RequestBody Excursie excursie) {
        System.out.println("Updating excursie");
        var result = repoExcursie.update(excursie); // empty if successful, the entity if not
        if (result.isEmpty())
            return new ResponseEntity<>(id, HttpStatus.OK);
        else
            return new ResponseEntity<>(id, HttpStatus.NOT_FOUND);
    }

    @GetMapping
    public Iterable<?> findAll() {
        System.out.println("Finding all excursii");
        return repoExcursie.findAll();
    }

    @GetMapping("/{id}")
    public ResponseEntity<?> findById(@PathVariable UUID id) {
        System.out.println("Finding excursie by ID");
        var result = repoExcursie.findOne(id);
        return result.map(excursie -> new ResponseEntity<>(excursie, HttpStatus.OK))
                .orElseGet(() -> new ResponseEntity<>(HttpStatus.NOT_FOUND));
    }
}
