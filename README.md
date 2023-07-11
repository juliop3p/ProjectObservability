# ProjectObservability


### Biblioteca de observabilidade objetivos
- [ ] Aplicações vão ter logs default já no prometheus e splunk
- [ ] Logs padronizados
- [ ] Facilitação para logar nas aplicações que consumirá essa lib
- [ ] Precisará apenas referenciar o projeto e fazer a injeção de dependência e passar as configurações (url splunk, token e etc...)
- [ ] Facilidade para integração


### Quais pontos devo me preocupar em realizar os logs?
- [ ] Logs das requisições da minha aplicação
- [ ] Logs das requisições feitas para apis externas
- [ ] Logs de erros e exceções (informações detalhadas sobre erros e exceções que ocorrem na aplicação, incluindo mensagens de erro, pilha de chamadas e dados relevantes para facilitar a identificação e solução de problemas)
- [ ] Logs de desempenho (métricas relacionadas ao desempenho da aplicação, como tempo de resposta, tempos de execução de consultas, utilização de recursos (CPU, memória, disco))
- [ ] Logs de infraestrutura (Registre informações sobre a infraestrutura em que sua aplicação está sendo executada, como eventos de inicialização, reinicialização, escalonamento de recursos, falhas de componentes, entre outros. )

### Biblioteca de observabilidade responsabilidades
#### Splunk:
- Logs detalhados de eventos e erros: O Splunk é uma plataforma de análise de dados e pode lidar com volumes grandes e variados de logs. Você pode enviar logs detalhados, incluindo informações sobre eventos, erros, exceções e atividades específicas da sua aplicação.
- Logs de segurança e conformidade: Se você precisa monitorar e auditar atividades de segurança, violações de políticas, tentativas de acesso não autorizadas, entre outros, o Splunk pode ser uma escolha adequada.
- Logs de negócio e métricas personalizadas: Se você deseja registrar informações específicas de negócio e criar métricas personalizadas para análise e tomada de decisões, o Splunk pode ser uma boa opção.

#### Prometheus:
- Métricas de desempenho e saúde do sistema: O Prometheus é uma ferramenta focada em coletar e armazenar métricas de sistemas e aplicações. Você pode enviar métricas relacionadas ao desempenho da aplicação, utilização de recursos, tempos de resposta, entre outros.
- Métricas de infraestrutura: Se você deseja monitorar métricas relacionadas à infraestrutura em que sua aplicação está sendo executada, como uso de CPU, memória, disco, entre outros, o Prometheus pode ser uma escolha adequada.
- Métricas de escalabilidade e disponibilidade: Se você precisa monitorar a escalabilidade e a disponibilidade da sua aplicação, o Prometheus pode ajudar a coletar métricas relevantes, como o número de requisições por segundo, tempo médio de resposta, erros de rede, entre outros.

### Tenho um erro na minha aplicação, o que preciso verificar?
1° A aplicação está de pé?
2° Tivemos deploy?
3° CPU/Memória/Disco está OK?
4° Tempo de resposta está OK?
5° Erro ocorre em todos os serves/clusters?
6° Balancemanto está ok?
7° Os serviços que consumimos está de pé?
8° Qual endpoint está gerando erro?
9° Em que momento o erro começou? (grafico de tempo)

<details>
  <summary>Pontos com descrição</summary>
  
1° A aplicação está de pé?
Verifique o status da aplicação, como verificar se o processo está em execução e se a aplicação está respondendo às requisições. Isso pode ser feito através do monitoramento de processos ou por meio de ping/health checks regulares.

2° Tivemos deploy?
Monitore o processo de deploy para detectar possíveis problemas ou falhas durante a implantação da nova versão da aplicação. Isso pode ser feito por meio de registros de eventos de deploy e acompanhamento de métricas específicas durante o processo.

3° CPU/Memória/Disco está OK?
Acompanhe o uso de recursos do sistema, como CPU, memória e disco, para identificar possíveis gargalos ou problemas de capacidade. Isso pode ajudar a determinar se o erro está relacionado a recursos insuficientes ou excessivamente utilizados.

4° Tempo de resposta está OK?
Monitore o tempo de resposta das requisições da aplicação para identificar possíveis atrasos ou aumento no tempo de resposta. Isso pode ajudar a detectar problemas de desempenho ou gargalos na aplicação.

5° Erro ocorre em todos os servidores/clusters?
Verifique se o erro está ocorrendo em todos os servidores ou apenas em um subconjunto específico. Isso pode ajudar a identificar problemas de configuração, conectividade ou inconsistências entre os ambientes.

6° Balanceamento está OK?
Acompanhe o funcionamento e o desempenho do balanceador de carga para garantir que ele esteja distribuindo as requisições corretamente entre os servidores. Isso pode envolver a monitoração de métricas do balanceador de carga e a verificação de logs de eventos relacionados ao balanceamento.

7° Os serviços que consumimos estão de pé?
Monitore a disponibilidade dos serviços externos que a sua aplicação consome. Isso pode ser feito por meio de verificações regulares de integridade desses serviços e a captura de eventos ou erros relacionados a falhas de conexão ou problemas nos serviços consumidos.

8° Qual endpoint está gerando erro?
Acompanhe as informações detalhadas sobre os erros, incluindo a rota ou endpoint específico que está gerando o erro. Isso pode ajudar na identificação rápida do ponto de falha e direcionar a investigação e resolução do problema.

9° Em que momento o erro começou? (gráfico de tempo)
Registre a data e hora de início do erro e utilize gráficos de tempo para visualizar o comportamento e as mudanças no sistema antes, durante e após o início do erro. Isso pode ajudar a identificar eventos ou alterações que possam ter desencadeado o erro.

### outros pontos para considerar
1° Taxa de erros e exceções: Acompanhe a taxa de erros e exceções ocorridas na aplicação. Isso inclui capturar e registrar informações sobre erros não tratados, exceções lançadas e problemas relacionados ao fluxo de execução do código.

2° Latência de banco de dados: Monitore o tempo de resposta das consultas ao banco de dados para identificar gargalos de desempenho, problemas de índice ou consultas ineficientes.

3° Consumo de recursos de terceiros: Se sua aplicação depende de serviços de terceiros, monitore o consumo desses serviços, incluindo tempo de resposta, taxa de erros e disponibilidade. Isso pode ajudar a identificar problemas nos serviços externos que podem afetar o desempenho ou a disponibilidade da sua aplicação.

4° Logs de segurança: Registre informações relevantes para a segurança da aplicação, como tentativas de autenticação mal-sucedidas, acessos não autorizados ou atividades suspeitas. Isso pode auxiliar na detecção e investigação de possíveis ameaças à segurança.

5° Monitoramento de transações: Acompanhe o fluxo de transações da aplicação, desde o início até a conclusão, registrando informações importantes em cada etapa. Isso pode incluir a duração da transação, os passos executados e os resultados obtidos, permitindo uma visão completa do desempenho e integridade das transações.

6° Monitoramento de fila de mensagens: Se sua aplicação utiliza filas de mensagens para processamento assíncrono, monitore o tamanho da fila, o tempo de processamento das mensagens e possíveis atrasos na entrega. Isso pode ajudar a identificar problemas de processamento ou gargalos na comunicação assíncrona.

7° Monitoramento de cache: Se sua aplicação utiliza caches para melhorar o desempenho, monitore o uso e a efetividade do cache, incluindo taxas de acertos e falhas de cache, tempo de acesso ao cache e evict rate. Isso pode ajudar a identificar problemas de cache, como invalidações frequentes ou políticas inadequadas de cache.
</details>


### Quero acompanhar o uso da aplicação quais graficos são interessantes?
1° Quantidade de requests.
2° Quantidade de sucesso.
3° Quantidade de erro.
4° Quais são as rotas mais acessadas?
5° Quais são as rotas que mais apresentam erro?
6° Média de requests por minuto.
7° Em quais momentos do dia temos mais requisições.
8° Novas pessoas na aplicação como acompanhar o aumento desses novos usuários?

<details>
  <summary>Pontos para considerar</summary>
  
  Quantidade de usuários ativos: Acompanhe o número de usuários únicos que acessam a aplicação em um determinado período de tempo. Isso pode ajudar a identificar tendências de crescimento e avaliar o impacto de ações de marketing ou lançamento de novos recursos.

Taxa de conversão: Se a aplicação possui objetivos de conversão, como cadastro de usuários ou compras, acompanhe a taxa de conversão para identificar possíveis pontos de melhoria no funil de conversão.

Tempo médio de uso: Calcule o tempo médio que os usuários passam na aplicação. Isso pode indicar o nível de engajamento e o valor percebido pelos usuários em relação ao seu produto ou serviço.

Retenção de usuários: Acompanhe a taxa de retenção de usuários ao longo do tempo. Isso pode ajudar a identificar se os usuários estão retornando à aplicação com frequência e se estão encontrando valor contínuo.

Análise de funis: Se a aplicação possui fluxos de navegação específicos, como um processo de compra ou preenchimento de um formulário, analise os funis de conversão para identificar etapas onde os usuários estão abandonando ou enfrentando dificuldades.

Segmentação de usuários: Considere segmentar os usuários com base em características demográficas, comportamentais ou de uso da aplicação. Isso pode permitir uma análise mais granular do desempenho da aplicação em diferentes grupos de usuários.

Origem do tráfego: Acompanhe a origem do tráfego da aplicação, como mecanismos de busca, redes sociais ou campanhas de marketing. Isso pode ajudar a avaliar a eficácia das estratégias de aquisição de usuários e direcionar esforços para as fontes mais eficientes.

Métricas de engajamento: Considere métricas específicas de engajamento, como número de curtidas, compartilhamentos, comentários ou avaliações. Essas métricas podem indicar o nível de interação e envolvimento dos usuários com o conteúdo ou funcionalidades da aplicação.
</details>


Splunk:

Registre informações detalhadas sobre as requisições de entrada na sua API, incluindo caminho da rota, código de status, tempo de resposta, informações de autenticação, parâmetros, entre outros. Use esses logs para criar gráficos que mostrem as principais rotas com sucesso e as principais rotas com erro.
Para as chamadas a APIs externas, registre informações relevantes, como URL da API externa, código de status da resposta, tempo de resposta, dados enviados e recebidos. Isso pode ajudar a identificar problemas de integração com APIs externas e acompanhar a saúde dessas integrações.


Prometheus:

Utilize métricas relacionadas ao desempenho da sua aplicação para criar gráficos no Prometheus. Por exemplo, você pode monitorar a CPU e a memória utilizadas pela sua aplicação, bem como o uso de clusters, se aplicável.
Considere adicionar métricas de tempo de resposta da sua API e das chamadas a APIs externas. Isso permitirá que você monitore a performance das requisições e identifique possíveis gargalos ou atrasos.
Além das ideias acima, aqui estão algumas outras sugestões de gráficos que você pode criar:

Contagem de requisições por rota ou por método HTTP (GET, POST, etc.).
Histograma de tempos de resposta das requisições, permitindo visualizar a distribuição dos tempos de resposta e identificar possíveis outliers.
Gráfico de erros por tipo ou por rota, ajudando a identificar os tipos de erros mais comuns ou as rotas que apresentam maior número de erros.
Taxa de sucesso das requisições por rota, mostrando a porcentagem de requisições bem-sucedidas em relação ao total.
