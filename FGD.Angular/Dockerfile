#==================== Building Stage ================================================ 
 
FROM stefanscherer/node-windows as node

Run dir

RUN mkdir app

Run dir

WORKDIR ./app

Run dir

#Run cd ./app
 
COPY . .

RUN npm i 

RUN dir

EXPOSE 4200

CMD ["dir"]

CMD ["npm","start"]