[HttpGet("{id}")]
public IActionResult ObterPorId(int id)
{
    var tarefa = _context.Tarefas.Find(id);

    if (tarefa == null)
        return NotFound();

    return Ok(tarefa);
}

[HttpGet("ObterTodos")]
public IActionResult ObterTodos()
{
    var tarefas = _context.Tarefas.ToList();
    return Ok(tarefas);
}

[HttpGet("ObterPorTitulo")]
public IActionResult ObterPorTitulo(string titulo)
{
    var tarefas = _context.Tarefas.Where(t => t.Titulo.Contains(titulo)).ToList();
    return Ok(tarefas);
}

[HttpPost]
public IActionResult Criar(Tarefa tarefa)
{
    if (tarefa.Data == DateTime.MinValue)
        return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

    _context.Tarefas.Add(tarefa);
    _context.SaveChanges();

    return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
}

[HttpPut("{id}")]
public IActionResult Atualizar(int id, Tarefa tarefa)
{
    var tarefaBanco = _context.Tarefas.Find(id);

    if (tarefaBanco == null)
        return NotFound();

    if (tarefa.Data == DateTime.MinValue)
        return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

    tarefaBanco.Titulo = tarefa.Titulo;
    tarefaBanco.Descricao = tarefa.Descricao;
    tarefaBanco.Data = tarefa.Data;
    tarefaBanco.Status = tarefa.Status;

    _context.SaveChanges();

    return Ok();
}

[HttpDelete("{id}")]
public IActionResult Deletar(int id)
{
    var tarefaBanco = _context.Tarefas.Find(id);

    if (tarefaBanco == null)
        return NotFound();

    _context.Tarefas.Remove(tarefaBanco);
    _context.SaveChanges();

    return NoContent();
}