select assunto, ano, count(*) as quantidade
from atendimentos
group by assunto, ano
having count(*) > 3
order by ano asc, quantidade desc