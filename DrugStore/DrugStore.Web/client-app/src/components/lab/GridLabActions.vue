<template>
  <v-container>
    <v-layout>
      <v-card>
        <v-card-title>
          Laboratorios
          <div class="flex-grow-1"></div>
          <v-text-field
            v-model="search"
            append-icon="search"
            label="Buscar"
            single-line
            hide-details
          ></v-text-field>
          <v-spacer></v-spacer>
 
          <v-dialog v-model="dialogNewLab" persistent max-width="600px">
            <template v-slot:activator="{ on }">
              <v-btn color="primary" dark class="mb-2" v-on="on">Nuevo Lab</v-btn>
            </template>
              <FormNewLab                                 
                @save="addNewLab"
                @cancel="closeDialog"
              />       
          </v-dialog>         
        </v-card-title>                       
       
        <v-data-table
          :headers="headersDataTable"
          :items="labs"
          :search="search"
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
              @click="showLabforEdit(item)"
            >
              edit
            </v-icon>                                                                
            <template v-if="item.condition">
              <v-icon
              small
              @click="activateDeactivateShowLab(2,item)"
            >
              block
            </v-icon>
            </template>
             <template v-else>
              <v-icon
              small
              @click="activateDeactivateShowLab(1,item)"
            >
              check
            </v-icon>
            </template>
          </template>  

        </v-data-table>
      </v-card>

      <template>            
        <v-dialog v-model="dialogEdit" max-width="600px">       
          <FormEditLab
            :laboratory="lab"                     
            @save="updateLab"
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
              <v-card-title class="headline" v-if="adAction === 1">¿Activar Laboratorio?</v-card-title>
              <v-card-title class="headline" v-if="adAction === 2">¿Desactivar Laboratorio?</v-card-title>
              <v-card-text>
                Estas a punto de
                <span v-if="adAction === 1">Activar</span>
                <span v-if="adAction === 2">Desactivar</span>
                el laboratorio {{ adName}}
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
  //importar formularios de editar y agregar
  import FormNewLab from '@/components/lab/FormNewLab'
  import FormEditLab from '@/components/lab/FormEditLab'
  
  export default {
    components: {
      FormNewLab,
      FormEditLab
    },
    data () {
      return {        
        headersDataTable:[
          {text:'ID', value:'idLaboratory'},
          {text:'Nombre', value:'laboratoryName'},
          {text:'Descripcion', value:'description'},         
          {text:'Estado', value:'condition'},
          {text: 'Actions', value: 'action', sortable: false }
        ],
        labs:[],        
        lab:'',        
        search:'',   
        dialogNewLab:false,
        dialogEdit:false,
        adModal:0,  //activar o desactivar el modal      
        adAction:0,
        adName:'',
        adId:''
      }
    },
    created () {
      this.listLabs();
    },
    methods: {
      listLabs() {
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        axios.get('api/laboratory/list',headers)
        .then(function (response) {
          // handle success
          me.labs = response.data      
          console.log(me.labs)                                              
          })
        .catch(function (error) {
          // handle error          
          console.log(error);   
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }      
        })
      },
      addNewLab(lab){   
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        axios.post('api/laboratory/Create/',
        {                
          'LaboratoryName':lab.laboratoryName,
          'Description':lab.description, 
          'Condition':lab.condition
        },
          headers)
        .then(function (response) {
          // handle success
          console.log('success') 
          me.listLabs()
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
      showLabforEdit(item){           
        console.log(item)         
        this.lab = item       
        this.dialogEdit = true      
      },
      updateLab(item){
        this.lab = item.laboratory
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}              
        axios.put('api/laboratory/updateLab',
        {
          'IdLaboratory':me.lab.idLaboratory,
          'LaboratoryName':me.lab.laboratoryName,       
          'Description':me.lab.description,          
          'Condition':me.lab.condition
        },headers)
        .then(function (response) {
          // handle success
          console.log('Update Successful')       
          me.closeDialog()     
          me.listLabs()                                                                      
        })
        .catch(function (error) {
          // handle error          
          console.log(error);  
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }       
        })
      },
      activateDeactivateShowLab(action, item){
        this.adModal = 1 // se muestra el modal de confirmacion
        this.adName = item.laboratoryName
        this.adId =  item.idLaboratory     

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
        axios.put('api/laboratory/Activate/'+ this.adId, {}, headers)
        .then(function (response) {
          // handle success
          me.adModal = 0
          me.adAction = 0
          me.adName = ""
          me.adId = ""
          me.listLabs()                                                                  
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
        axios.put('api/laboratory/Deactivate/'+ this.adId, {}, headers)
        .then(function (response) {
          // handle success         
          me.adModal = 0
          me.adAction = 0
          me.adName = ""
          me.adId = ""
          me.listLabs()                                                                  
          })
        .catch(function (error) {
          // handle error          
          console.log(error);  
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }       
        })
      },
      closeDialog(switchBool){
        this.dialogEdit = switchBool
        this.dialogNewLab = switchBool
      },
    },
  }
</script>