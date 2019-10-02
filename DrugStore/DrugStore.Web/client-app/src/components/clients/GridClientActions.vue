<template>
  <v-container>
    <v-layout>
      <v-card flat color="white">
        <v-card-title>Clientes
          <v-spacer></v-spacer>
          <v-text-field
            v-model="search"
            append-icon="search"
            label="Search"
            single-line              
          ></v-text-field>
          <v-spacer></v-spacer>          

          <!-- Dialog for adding new product -->
          <v-dialog v-model="dialogNewClient" persistent max-width="600px">
            <template v-slot:activator="{ on }">
              <v-btn color="primary" dark class="mb-2" v-on="on">Nuevo Cliente</v-btn>
            </template>
              <FormNewClient                              
                @save="addNewClient"
                @cancel="closeDialog"
              />       
          </v-dialog>         
        </v-card-title> 
        
        <v-data-table        
          :headers="headersDataGrid"
          :items="clients"
          :search="search"
          class="elevation-1"      
        >                                                          
          
          <template v-slot:item.condition="{item}">                
              <div v-if="item.condition">
                <span class="blue--text">Activo</span>
              </div>
              <div v-else>
                <span class="red--text">Inactivo</span>
              </div>
          </template>

          <template v-slot:item.action="{ item }">         
            <v-icon
              small
              class="mr-2"                  
              @click="showClientforEdit(item)"
            >
              edit
            </v-icon>                                                                
            <template v-if="item.condition">
              <v-icon
                small
                @click="activateDeactivateShowClient(2,item)"
              >
                block
              </v-icon>
            </template>
             <template v-else>
              <v-icon
                small
                @click="activateDeactivateShowClient(1,item)"
              >
                check
              </v-icon>
            </template>
          </template>  
        </v-data-table>
      </v-card>

      <template>            
        <v-dialog v-model="dialogEdit" max-width="600px">       
          <FormEditClient
            :client="client"                   
            @save="updateClient"
            @cancel="closeDialog"
          />  
                      
        </v-dialog>                              
      </template>

      <template>
        <v-dialog 
          v-model="adModal"
          max-width="290"
          >
            <v-card>
              <v-card-title class="headline" v-if="adAction === 1">¿Activar Cliente?</v-card-title>
              <v-card-title class="headline" v-if="adAction === 2">¿Desactivar Cliente?</v-card-title>
              <v-card-text>
                Estas a punto de
                <span v-if="adAction === 1">Activar</span>
                <span v-if="adAction === 2">Desactivar</span>
                el cliente {{ adName}}
              </v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="green darken-1" flat="flat" @click="adModal=0">Cancelar</v-btn>
                <v-btn v-if="adAction== 1" color="green darken-4" @click="activate()">Aceptar</v-btn>
                <v-btn v-if="adAction== 2" color="green darken-4" @click="deactivate()">Aceptar</v-btn>
              </v-card-actions>
            </v-card>
        </v-dialog>
      </template>

    </v-layout>
  </v-container>
</template>

<script> 
  import axios from 'axios'   
  import FormNewClient from '@/components/clients/FormNewClient'
  import FormEditClient from '@/components/clients/FormEditClient'

  export default { 
    components: {
      FormNewClient,
      FormEditClient
    },
    data() {
      return {
        headersDataGrid:[
          {text:'ID', value:'idClient'},
          {text:'Nombre', value:'name'},
          {text:'Apellidos', value:'lastName'},
          {text:'Tipo de Documento', value:'documentType'},
          {text:'Numero De Documento', value:'documentNumber'},
          {text:'Numero de telefono', value:'phoneNumber'},
          {text:'Estado', value:'condition'},
          { text: 'Actions', value: 'action', sortable: false }
        ],
        clients:[],
        client:'',        
        search:'',        
        dialogNewClient:false,
        dialogEdit:false,
        adModal:0,  //activar o desactivar el modal      
        adAction:0,
        adName:'',
        adId:''
      }
    },
    created () {
      this.listClients()     
    },   
    methods: {
      listClients() {
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        axios.get('api/client/list',headers)
        .then(function (response) {
          // handle success
          me.clients = response.data                                                    
          })
        .catch(function (error) {
          // handle error          
          console.log(error);   
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }      
        })
      },        
      addNewClient(client){   
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        axios.post('api/Client/Create/',
          {            
            'Name':client.name,
            'LastName':client.lastName,
            'DocumentType':client.documentType,            
            'DocumentNumber':client.documentNumber,
            'PhoneNumber':client.phoneNumber,
            'Condition':client.condition
          },
          headers)
        .then(function (response) {
          // handle success
          console.log('success') 
          me.listClients()
          me.closeDialog()                                                    
          })
        .catch(function (error) {
          // handle error          
          console.log(error);  
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }       
        })
      },
      //get by id
      showClientforEdit(item){                    
        this.client = item       
        this.dialogEdit = true      
      },
      updateClient(item){
        this.client = item.client
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}              
        axios.put('api/client/update',
        {
          'IdProduct':me.client.idClient,
          'Name':me.client.name,
          'LastName':me.client.lastName,
          'DocumentType':me.client.documentType,            
          'DocumentNumber':me.client.documentNumber,
          'PhoneNumber':me.client.phoneNumber,
          'Condition':me.client.condition
        },headers)
        .then(function (response) {
          // handle success
          console.log('Update Successful')       
          me.closeDialog()     
          me.listClients()                                                                      
        })
        .catch(function (error) {
          // handle error          
          console.log(error);  
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }       
        })
      },
      activateDeactivateShowClient(action, item){
        this.adModal = 1 // se muestra el modal de confirmacion
        this.adName = item.name + " " + item.lastName
        this.adId =  item.idClient      

        if(action === 1)
        {
          this.adAction = 1
        }
        else if(action === 2)
        {
           this.adAction = 2
        }
        else{
          this.adModal = 0
        }
      },
      activate(){
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        axios.put('api/client/Activate/'+ this.adId, {}, headers)
        .then(function (response) {
          // handle success     
          me.adModal = 0
          me.adAction = 0
          me.adName = ""
          me.adId = ""
          me.listClients()                                                                  
          })
        .catch(function (error) {
          // handle error          
          console.log(error);  
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }       
        })
      },
      deactivate(){
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        axios.put('api/client/Deactivate/'+ this.adId, {}, headers)
        .then(function (response) {
          // handle success         
          me.adModal = 0
          me.adAction = 0
          me.adName = ""
          me.adId = ""
          me.listClients()                                                                 
          })
        .catch(function (error) {
          // handle error          
          console.log(error);  
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }       
        })
      },
      clearForm(){

      },
      closeDialog(switchBool){
        this.dialogEdit = switchBool
        this.dialogNewClient = switchBool
      }      
    },
  }
</script>
